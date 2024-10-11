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
    /// Implements the IInterestService interface for Interest business logic.
    /// </summary>
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the InterestService class.
        /// </summary>
        /// <param name="interestRepository">The Interest repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public InterestService(IInterestRepository interestRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _interestRepository = interestRepository;
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
        public async Task<InterestDto> GetByIdAsync(string id)
        {
            var interest = await _interestRepository.GetByIdAsync(id);
            if (interest == null)
            {
                throw new KeyNotFoundException($"Interest with ID {id} was not found.");
            }

            return interest.Adapt<InterestDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InterestDto>> GetAllAsync()
        {
            var interests = await _interestRepository.GetAllAsync();
            return interests.Adapt<IEnumerable<InterestDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<InterestDto> CreateAsync(CreateInterestDto createDto)
        {
            // Map DTO to domain model
            var interest = createDto.Adapt<Interest>(_mapConfig);

            // Set creation timestamp and default values
            interest.CreatedAt = DateTime.UtcNow;
            interest.IsActive = createDto.IsActive ?? true;

            // Insert the new interest into the repository
            await _interestRepository.CreateAsync(interest);

            // Publish creation event to Kafka
            var message = interest.Adapt<InterestDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return interest.Adapt<InterestDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateInterestDto updateDto)
        {
            // Retrieve existing interest
            var existingInterest = await _interestRepository.GetByIdAsync(id);
            if (existingInterest == null)
            {
                throw new KeyNotFoundException($"Interest with ID {id} was not found.");
            }

            // Map update DTO to existing interest
            updateDto.Adapt(existingInterest, _mapConfig);
            existingInterest.UpdatedAt = DateTime.UtcNow;

            // Update the interest in the repository
            await _interestRepository.UpdateAsync(existingInterest);

            // Publish update event to Kafka
            var message = existingInterest.Adapt<InterestDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing interest
            var existingInterest = await _interestRepository.GetByIdAsync(id);
            if (existingInterest == null)
            {
                throw new KeyNotFoundException($"Interest with ID {id} was not found.");
            }

            // Delete the interest from the repository
            await _interestRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Interest with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}