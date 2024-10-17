using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using Confluent.Kafka;
using System.Text.Json;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the <see cref="IAlertMessageService"/> interface for AlertMessage business logic.
    /// </summary>
    public class AlertMessageService : IAlertMessageService, IDisposable
    {
        private readonly IAlertMessageRepository _alertMessageRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMessageService"/> class.
        /// </summary>
        /// <param name="alertMessageRepository">The AlertMessage repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public AlertMessageService(
            IAlertMessageRepository alertMessageRepository,
            TypeAdapterConfig mapConfig,
            IConfiguration configuration)
        {
            _alertMessageRepository = alertMessageRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for AlertMessageService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("AlertMessageTopic")
                          ?? throw new ArgumentException("Kafka topic for AlertMessageService is not configured.");
        }

        /// <inheritdoc />
        public async Task<AlertMessageDto> GetByIdAsync(string id)
        {
            var alertMessage = await _alertMessageRepository.GetByIdAsync(id);
            if (alertMessage == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {id} was not found.");
            }

            return alertMessage.Adapt<AlertMessageDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AlertMessageDto>> GetAllAsync()
        {
            var alertMessages = await _alertMessageRepository.GetAllAsync();
            return alertMessages.Adapt<IEnumerable<AlertMessageDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AlertMessageDto> CreateAsync(CreateAlertMessageDto createDto)
        {
            // Map DTO to domain model
            var alertMessage = createDto.Adapt<AlertMessage>(_mapConfig);

            // Set creation timestamp and default values
            alertMessage.CreatedAt = DateTime.UtcNow;
            alertMessage.Status = MessageStatus.Sent; // Beispielstatus

            // Insert the new AlertMessage into the repository
            await _alertMessageRepository.CreateAsync(alertMessage);

            // Publish creation event to Kafka
            var alertMessageDto = alertMessage.Adapt<AlertMessageDto>(_mapConfig);
            var message = JsonSerializer.Serialize(alertMessageDto);
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return alertMessageDto;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UpdateAlertMessageDto updateDto)
        {
            // Retrieve existing AlertMessage
            var existingAlert = await _alertMessageRepository.GetByIdAsync(updateDto.Id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {updateDto.Id} was not found.");
            }

            // Map update DTO to existing AlertMessage
            updateDto.Adapt(existingAlert, _mapConfig);
            existingAlert.UpdatedAt = DateTime.UtcNow;

            // Update the AlertMessage in the repository
            await _alertMessageRepository.UpdateAsync(existingAlert);

            // Publish update event to Kafka
            var alertMessageDto = existingAlert.Adapt<AlertMessageDto>(_mapConfig);
            var message = JsonSerializer.Serialize(alertMessageDto);
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing AlertMessage
            var existingAlert = await _alertMessageRepository.GetByIdAsync(id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {id} was not found.");
            }

            // Delete the AlertMessage from the repository
            await _alertMessageRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var deletionMessage = new
            {
                Event = "AlertMessageDeleted",
                AlertMessageId = id,
                Timestamp = DateTime.UtcNow
            };
            var messageJson = JsonSerializer.Serialize(deletionMessage);
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = messageJson });
        }

        /// <summary>
        /// Disposes the Kafka producer and releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _kafkaProducer?.Flush(TimeSpan.FromSeconds(10));
                    _kafkaProducer?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}