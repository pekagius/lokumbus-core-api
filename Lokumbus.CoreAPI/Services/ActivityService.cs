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
    /// Implements the IActivityService interface für Activity business logic.
    /// </summary>
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the ActivityService class.
        /// </summary>
        /// <param name="activityRepository">The Activity repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public ActivityService(IActivityRepository activityRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _activityRepository = activityRepository;
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
        public async Task<ActivityDto> GetByIdAsync(string id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} was not found.");
            }

            return activity.Adapt<ActivityDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            var activities = await _activityRepository.GetAllAsync();
            return activities.Adapt<IEnumerable<ActivityDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<ActivityDto> CreateAsync(CreateActivityDto createDto)
        {
            // Map DTO to domain model
            var activity = createDto.Adapt<Activity>(_mapConfig);

            // Set creation timestamp und Standardwerte
            activity.CreatedAt = DateTime.UtcNow;
            activity.IsActive = true;

            // Insert the new activity into the repository
            await _activityRepository.CreateAsync(activity);

            // Publish creation event to Kafka
            var activityDto = activity.Adapt<ActivityDto>(_mapConfig);
            var message = JsonSerializer.Serialize(activityDto);
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return activityDto;
        }

        /// <inheritdoc />
        public async Task<ActivityDto> UpdateAsync(string id, UpdateActivityDto updateDto)
        {
            // Retrieve existing activity
            var existingActivity = await _activityRepository.GetByIdAsync(id);
            if (existingActivity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} was not found.");
            }

            // Map update DTO to existing activity
            updateDto.Adapt(existingActivity, _mapConfig);
            existingActivity.UpdatedAt = DateTime.UtcNow;

            // Update the activity in the repository
            await _activityRepository.UpdateAsync(existingActivity);

            // Optional: Beziehung zur Event-Modell prüfen oder herstellen
            // Beispiel: Wenn Activity mit Event verknüpft ist, kann hier die Beziehung aktualisiert werden

            // Publish update event to Kafka
            var activityDto = existingActivity.Adapt<ActivityDto>(_mapConfig);
            var message = JsonSerializer.Serialize(activityDto);
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return activityDto;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing activity
            var existingActivity = await _activityRepository.GetByIdAsync(id);
            if (existingActivity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {id} was not found.");
            }

            // Delete the activity from the repository
            await _activityRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Activity with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}