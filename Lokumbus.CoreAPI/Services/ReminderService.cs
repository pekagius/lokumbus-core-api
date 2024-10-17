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
/// Implements the <see cref="IReminderService"/> interface for Reminder business logic.
/// </summary>
public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReminderService"/> class.
    /// </summary>
    /// <param name="reminderRepository">The Reminder repository instance.</param>
    /// <param name="mapConfig">The Mapster configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public ReminderService(IReminderRepository reminderRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
    {
        _reminderRepository = reminderRepository;
        _mapConfig = mapConfig;

        // Configure Kafka producer
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
        };
        _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

        // Retrieve Kafka topic for ActivityService from configuration
        _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("ReminderTopic") 
                      ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
    }

    /// <inheritdoc />
    public async Task<ReminderDto?> GetByIdAsync(string id)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        return reminder?.Adapt<ReminderDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ReminderDto>> GetAllAsync()
    {
        var reminders = await _reminderRepository.GetAllAsync();
        return reminders.Adapt<IEnumerable<ReminderDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<ReminderDto> CreateAsync(CreateReminderDto createDto)
    {
        // Map DTO to domain model
        var reminder = createDto.Adapt<Reminder>(_mapConfig);

        // Set creation timestamp and default values
        reminder.CreatedAt = DateTime.UtcNow;
        reminder.IsActive = true;

        // Insert the new reminder into the repository
        await _reminderRepository.CreateAsync(reminder);

        // Publish creation event to Kafka
        var message = reminder.Adapt<ReminderDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

        return reminder.Adapt<ReminderDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(string id, UpdateReminderDto updateDto)
    {
        // Retrieve existing reminder
        var existingReminder = await _reminderRepository.GetByIdAsync(id);
        if (existingReminder == null)
        {
            throw new KeyNotFoundException($"Reminder with ID {id} was not found.");
        }

        // Map update DTO to existing reminder
        updateDto.Adapt(existingReminder, _mapConfig);
        existingReminder.UpdatedAt = DateTime.UtcNow;

        // Update the reminder in the repository
        await _reminderRepository.UpdateAsync(existingReminder);

        // Publish update event to Kafka
        var message = existingReminder.Adapt<ReminderDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        // Retrieve existing reminder
        var existingReminder = await _reminderRepository.GetByIdAsync(id);
        if (existingReminder == null)
        {
            throw new KeyNotFoundException($"Reminder with ID {id} was not found.");
        }

        // Delete the reminder from the repository
        await _reminderRepository.DeleteAsync(id);

        // Publish deletion event to Kafka
        var message = $"Reminder with ID {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}