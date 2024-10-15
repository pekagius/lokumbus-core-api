using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Lokumbus.CoreAPI.Helpers;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the ISponsorshipService interface for Sponsorship business logic.
    /// </summary>
    public class SponsorshipService : ISponsorshipService
    {
        private readonly ISponsorshipRepository _sponsorshipRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the SponsorshipService class.
        /// </summary>
        /// <param name="sponsorshipRepository">The Sponsorship repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public SponsorshipService(ISponsorshipRepository sponsorshipRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _sponsorshipRepository = sponsorshipRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic from configuration
            var topics = configuration.GetSection("KafkaSettings").GetSection("Topics").Get<string[]>();
            _kafkaTopic = topics != null && topics.Length > 0 ? topics[0] : throw new ArgumentException("Kafka topic is not configured.");
        }

        /// <inheritdoc />
        public async Task<SponsorshipDto> GetByIdAsync(string id)
        {
            var sponsorship = await _sponsorshipRepository.GetByIdAsync(id);
            if (sponsorship == null)
            {
                throw new KeyNotFoundException($"Sponsorship with ID {id} was not found.");
            }

            return sponsorship.Adapt<SponsorshipDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SponsorshipDto>> GetAllAsync()
        {
            var sponsorships = await _sponsorshipRepository.GetAllAsync();
            return sponsorships.Adapt<IEnumerable<SponsorshipDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<SponsorshipDto> CreateAsync(CreateSponsorshipDto createDto)
        {
            // Map DTO to domain model
            var sponsorship = createDto.Adapt<Sponsorship>(_mapConfig);

            // Set creation timestamp and default values
            sponsorship.CreatedAt = DateTime.UtcNow;
            sponsorship.IsActive = true;

            // Insert the new sponsorship into the repository
            await _sponsorshipRepository.CreateAsync(sponsorship);

            // Publish creation event to Kafka
            var message = sponsorship.Adapt<SponsorshipDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return sponsorship.Adapt<SponsorshipDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateSponsorshipDto updateDto)
        {
            // Retrieve existing sponsorship
            var existingSponsorship = await _sponsorshipRepository.GetByIdAsync(id);
            if (existingSponsorship == null)
            {
                throw new KeyNotFoundException($"Sponsorship with ID {id} was not found.");
            }

            // Map update DTO to existing sponsorship
            updateDto.Adapt(existingSponsorship, _mapConfig);
            existingSponsorship.UpdatedAt = DateTime.UtcNow;

            // Update the sponsorship in the repository
            await _sponsorshipRepository.UpdateAsync(existingSponsorship);

            // Publish update event to Kafka
            var message = existingSponsorship.Adapt<SponsorshipDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing sponsorship
            var existingSponsorship = await _sponsorshipRepository.GetByIdAsync(id);
            if (existingSponsorship == null)
            {
                throw new KeyNotFoundException($"Sponsorship with ID {id} was not found.");
            }

            // Delete the sponsorship from the repository
            await _sponsorshipRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Sponsorship with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}