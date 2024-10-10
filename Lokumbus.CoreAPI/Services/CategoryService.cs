using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services;

/// <summary>
/// Implements the ICategoryService interface for Category business logic.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initializes a new instance of the CategoryService class.
    /// </summary>
    /// <param name="categoryRepository">The Category repository instance.</param>
    /// <param name="mapConfig">The Mapster configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public CategoryService(ICategoryRepository categoryRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
    {
        _categoryRepository = categoryRepository;
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
    public async Task<CategoryDto> GetByIdAsync(string id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} was not found.");
        }

        return category.Adapt<CategoryDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetAllActiveAsync()
    {
        var categories = await _categoryRepository.GetAllActiveAsync();
        return categories.Adapt<IEnumerable<CategoryDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Adapt<IEnumerable<CategoryDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto createDto)
    {
        // Map DTO to domain model
        var category = createDto.Adapt<Category>(_mapConfig);

        // Set creation timestamp and default values
        category.CreatedAt = DateTime.UtcNow;
        category.IsActive = true;

        // Insert the new category into the repository
        await _categoryRepository.CreateAsync(category);

        // Publish creation event to Kafka
        var message = category.Adapt<CategoryDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

        return category.Adapt<CategoryDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(string id, UpdateCategoryDto updateDto)
    {
        // Retrieve existing category
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} was not found.");
        }

        // Map update DTO to existing category
        updateDto.Adapt(existingCategory, _mapConfig);
        existingCategory.UpdatedAt = DateTime.UtcNow;

        // Update the category in the repository
        await _categoryRepository.UpdateAsync(existingCategory);

        // Publish update event to Kafka
        var message = existingCategory.Adapt<CategoryDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        // Retrieve existing category
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} was not found.");
        }

        // Delete the category from the repository
        await _categoryRepository.DeleteAsync(id);

        // Publish deletion event to Kafka
        var message = $"Category with ID {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}