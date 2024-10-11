using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using Confluent.Kafka;
using Lokumbus.CoreAPI.Helpers;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the IAlertService interface for Alert business logic.
    /// </summary>
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the AlertService class.
        /// </summary>
        /// <param name="alertRepository">The Alert repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public AlertService(IAlertRepository alertRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _alertRepository = alertRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic from configuration
            var topics = configuration.GetSection("KafkaSettings:Topics").Get<string[]>();
            _kafkaTopic = topics != null && topics.Length > 0 ? topics[0] : throw new ArgumentException("Kafka topic is not configured.");
        }

        /// <inheritdoc />
        public async Task<AlertDto> GetByIdAsync(string id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
            {
                throw new KeyNotFoundException($"Alert with ID {id} was not found.");
            }

            return alert.Adapt<AlertDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AlertDto>> GetAllAsync()
        {
            var alerts = await _alertRepository.GetAllAsync();
            return alerts.Adapt<IEnumerable<AlertDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AlertDto> CreateAsync(CreateAlertDto createDto)
        {
            // Map DTO to domain model
            var alert = createDto.Adapt<Alert>(_mapConfig);

            // Set creation timestamp and default values
            alert.CreatedAt = DateTime.UtcNow;
            alert.IsDismissed = false;

            // Insert the new alert into the repository
            await _alertRepository.CreateAsync(alert);

            // Publish creation event to Kafka
            var message = alert.Adapt<AlertDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return alert.Adapt<AlertDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateAlertDto updateDto)
        {
            // Retrieve existing alert
            var existingAlert = await _alertRepository.GetByIdAsync(id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"Alert with ID {id} was not found.");
            }

            // Map update DTO to existing alert
            updateDto.Adapt(existingAlert, _mapConfig);
            existingAlert.UpdatedAt = DateTime.UtcNow;

            // Update the alert in the repository
            await _alertRepository.UpdateAsync(existingAlert);

            // Publish update event to Kafka
            var message = existingAlert.Adapt<AlertDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing alert
            var existingAlert = await _alertRepository.GetByIdAsync(id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"Alert with ID {id} was not found.");
            }

            // Delete the alert from the repository
            await _alertRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Alert with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}