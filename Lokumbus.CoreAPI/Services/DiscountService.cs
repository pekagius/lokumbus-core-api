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
    /// Implements the IDiscountService interface for Discount business logic.
    /// </summary>
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the DiscountService class.
        /// </summary>
        /// <param name="discountRepository">The Discount repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public DiscountService(IDiscountRepository discountRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _discountRepository = discountRepository;
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
        public async Task<DiscountDto> GetByIdAsync(string id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            if (discount == null)
            {
                throw new KeyNotFoundException($"Discount with ID {id} was not found.");
            }

            return discount.Adapt<DiscountDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DiscountDto>> GetAllAsync()
        {
            var discounts = await _discountRepository.GetAllAsync();
            return discounts.Adapt<IEnumerable<DiscountDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<DiscountDto> CreateAsync(CreateDiscountDto createDto)
        {
            // Map DTO to domain model
            var discount = createDto.Adapt<Discount>(_mapConfig);

            // Set creation timestamp and default values
            discount.CreatedAt = DateTime.UtcNow;
            discount.UpdatedAt = DateTime.UtcNow;

            // Insert the new discount into the repository
            await _discountRepository.CreateAsync(discount);

            // Publish creation event to Kafka
            var message = discount.Adapt<DiscountDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return discount.Adapt<DiscountDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateDiscountDto updateDto)
        {
            // Retrieve existing discount
            var existingDiscount = await _discountRepository.GetByIdAsync(id);
            if (existingDiscount == null)
            {
                throw new KeyNotFoundException($"Discount with ID {id} was not found.");
            }

            // Map update DTO to existing discount
            updateDto.Adapt(existingDiscount, _mapConfig);
            existingDiscount.UpdatedAt = DateTime.UtcNow;

            // Update the discount in the repository
            await _discountRepository.UpdateAsync(existingDiscount);

            // Publish update event to Kafka
            var message = existingDiscount.Adapt<DiscountDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing discount
            var existingDiscount = await _discountRepository.GetByIdAsync(id);
            if (existingDiscount == null)
            {
                throw new KeyNotFoundException($"Discount with ID {id} was not found.");
            }

            // Delete the discount from the repository
            await _discountRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Discount with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}