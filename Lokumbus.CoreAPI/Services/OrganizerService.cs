using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the IOrganizerService interface for Organizer business logic.
    /// </summary>
    public class OrganizerService : IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the OrganizerService class.
        /// </summary>
        /// <param name="organizerRepository">The Organizer repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public OrganizerService(IOrganizerRepository organizerRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _organizerRepository = organizerRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for ActivityService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("OrganizerTopic") 
                          ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
        }

        /// <inheritdoc />
        public async Task<OrganizerDto> GetByIdAsync(string id)
        {
            var organizer = await _organizerRepository.GetByIdAsync(id);
            if (organizer == null)
            {
                throw new KeyNotFoundException($"Organizer with ID {id} was not found.");
            }

            return organizer.Adapt<OrganizerDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrganizerDto>> GetAllAsync()
        {
            var organizers = await _organizerRepository.GetAllAsync();
            return organizers.Adapt<IEnumerable<OrganizerDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<OrganizerDto> CreateAsync(CreateOrganizerDto createDto)
        {
            // Map DTO to domain model
            var organizer = createDto.Adapt<Organizer>(_mapConfig);

            // Set creation timestamp and default values
            organizer.CreatedAt = DateTime.UtcNow;
            organizer.IsActive = true;
            organizer.IsVerified = false;

            // Insert the new organizer into the repository
            await _organizerRepository.CreateAsync(organizer);

            // Publish creation event to Kafka
            var message = organizer.Adapt<OrganizerDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return organizer.Adapt<OrganizerDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateOrganizerDto updateDto)
        {
            // Retrieve existing organizer
            var existingOrganizer = await _organizerRepository.GetByIdAsync(id);
            if (existingOrganizer == null)
            {
                throw new KeyNotFoundException($"Organizer with ID {id} was not found.");
            }

            // Map update DTO to existing organizer
            updateDto.Adapt(existingOrganizer, _mapConfig);
            existingOrganizer.UpdatedAt = DateTime.UtcNow;

            // Update the organizer in the repository
            await _organizerRepository.UpdateAsync(existingOrganizer);

            // Publish update event to Kafka
            var message = existingOrganizer.Adapt<OrganizerDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing organizer
            var existingOrganizer = await _organizerRepository.GetByIdAsync(id);
            if (existingOrganizer == null)
            {
                throw new KeyNotFoundException($"Organizer with ID {id} was not found.");
            }

            // Delete the organizer from the repository
            await _organizerRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Organizer with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}