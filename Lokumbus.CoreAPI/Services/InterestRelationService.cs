using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using Confluent.Kafka;
using System.Text.Json;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the <see cref="IInterestRelationService"/> interface for InterestRelation business logic.
    /// </summary>
    public class InterestRelationService : IInterestRelationService, IDisposable
    {
        private readonly IInterestRelationRepository _interestRelationRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestRelationService"/> class.
        /// </summary>
        /// <param name="interestRelationRepository">The InterestRelation repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public InterestRelationService(
            IInterestRelationRepository interestRelationRepository,
            TypeAdapterConfig mapConfig,
            IConfiguration configuration)
        {
            _interestRelationRepository = interestRelationRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for InterestRelationService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("InterestRelationTopic")
                          ?? throw new ArgumentException("Kafka topic for InterestRelationService is not configured.");
        }

        /// <inheritdoc />
        public async Task<InterestRelationDto?> GetByIdAsync(string id)
        {
            var interestRelation = await _interestRelationRepository.GetByIdAsync(id);
            return interestRelation?.Adapt<InterestRelationDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InterestRelationDto>> GetAllAsync()
        {
            var interestRelations = await _interestRelationRepository.GetAllAsync();
            return interestRelations.Adapt<IEnumerable<InterestRelationDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<InterestRelationDto> CreateAsync(CreateInterestRelationDto createDto)
        {
            // Map DTO to domain model
            var interestRelation = createDto.Adapt<InterestRelation>(_mapConfig);

            // Set creation timestamp and default values
            interestRelation.CreatedAt = DateTime.UtcNow;
            interestRelation.IsActive = true;

            // Insert the new InterestRelation into the repository
            await _interestRelationRepository.CreateAsync(interestRelation);

            // Adapt to DTO for response and Kafka message
            var interestRelationDto = interestRelation.Adapt<InterestRelationDto>(_mapConfig);

            // Serialize DTO to JSON
            var message = JsonSerializer.Serialize(interestRelationDto);

            // Publish creation event to Kafka
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return interestRelationDto;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateInterestRelationDto updateDto)
        {
            // Retrieve existing InterestRelation
            var existingInterestRelation = await _interestRelationRepository.GetByIdAsync(id);
            if (existingInterestRelation == null)
            {
                throw new KeyNotFoundException($"InterestRelation mit ID {id} wurde nicht gefunden.");
            }

            // Map update DTO to existing InterestRelation
            updateDto.Adapt(existingInterestRelation, _mapConfig);
            existingInterestRelation.UpdatedAt = DateTime.UtcNow;

            // Update the InterestRelation in the repository
            await _interestRelationRepository.UpdateAsync(existingInterestRelation);

            // Adapt to DTO for Kafka message
            var interestRelationDto = existingInterestRelation.Adapt<InterestRelationDto>(_mapConfig);

            // Serialize DTO to JSON
            var message = JsonSerializer.Serialize(interestRelationDto);

            // Publish update event to Kafka
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing InterestRelation
            var existingInterestRelation = await _interestRelationRepository.GetByIdAsync(id);
            if (existingInterestRelation == null)
            {
                throw new KeyNotFoundException($"InterestRelation mit ID {id} wurde nicht gefunden.");
            }

            // Delete the InterestRelation from the repository
            await _interestRelationRepository.DeleteAsync(id);

            // Create a deletion event message
            var deletionMessage = new
            {
                Event = "InterestRelationDeleted",
                InterestRelationId = id,
                Timestamp = DateTime.UtcNow
            };
            var messageJson = JsonSerializer.Serialize(deletionMessage);

            // Publish deletion event to Kafka
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
                    // Ensure all messages are sent before disposing
                    _kafkaProducer?.Flush(TimeSpan.FromSeconds(10));
                    _kafkaProducer?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}