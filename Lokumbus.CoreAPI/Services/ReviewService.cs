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
/// Implementiert das <see cref="IReviewService"/>-Interface für die Geschäftslogik von Reviews.
/// </summary>
public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="ReviewService"/>-Klasse.
    /// </summary>
    /// <param name="reviewRepository">Das Review-Repository-Interface.</param>
    /// <param name="mapConfig">Die Mapster-Konfiguration.</param>
    /// <param name="configuration">Die Anwendungskonfiguration.</param>
    public ReviewService(IReviewRepository reviewRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
    {
        _reviewRepository = reviewRepository;
        _mapConfig = mapConfig;

        // Konfigurieren des Kafka-Producers
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
        };
        _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

        // Abrufen des Kafka-Themas aus der Konfiguration
        var topics = configuration.GetSection("KafkaSettings").GetSection("Topics").Get<string[]>();
        _kafkaTopic = topics != null && topics.Length > 0
            ? topics[0]
            : throw new ArgumentException("Kafka topic is not configured.");
    }

    /// <inheritdoc />
    public async Task<ReviewDto?> GetByIdAsync(string id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        return review?.Adapt<ReviewDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return reviews.Adapt<IEnumerable<ReviewDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<ReviewDto> CreateAsync(CreateReviewDto createDto)
    {
        // Mapping von DTO zu Domain-Modell
        var review = createDto.Adapt<Review>(_mapConfig);

        // Setzen des Erstellungsdatums und Standardwerte
        review.CreatedAt = DateTime.UtcNow;
        review.IsActive = true;

        // Einfügen des neuen Reviews in das Repository
        await _reviewRepository.CreateAsync(review);

        // Veröffentlichen des Erstellungsereignisses in Kafka
        var message = review.Adapt<ReviewDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

        return review.Adapt<ReviewDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(string id, UpdateReviewDto updateDto)
    {
        // Abrufen des bestehenden Reviews
        var existingReview = await _reviewRepository.GetByIdAsync(id);
        if (existingReview == null)
        {
            throw new KeyNotFoundException($"Review with ID {id} was not found.");
        }

        // Mapping des Update-DTO auf das bestehende Review
        updateDto.Adapt(existingReview, _mapConfig);
        existingReview.UpdatedAt = DateTime.UtcNow;

        // Aktualisieren des Reviews im Repository
        await _reviewRepository.UpdateAsync(existingReview);

        // Veröffentlichen des Aktualisierungsereignisses in Kafka
        var message = existingReview.Adapt<ReviewDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        // Abrufen des bestehenden Reviews
        var existingReview = await _reviewRepository.GetByIdAsync(id);
        if (existingReview == null)
        {
            throw new KeyNotFoundException($"Review with ID {id} was not found.");
        }

        // Löschen des Reviews im Repository
        await _reviewRepository.DeleteAsync(id);

        // Veröffentlichen des Löschereignisses in Kafka
        var message = $"Review with ID {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}