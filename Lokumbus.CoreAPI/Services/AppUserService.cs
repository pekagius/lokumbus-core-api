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
/// Implements the IAppUserService interface for AppUser business logic.
/// </summary>
public class AppUserService : IAppUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initializes a new instance of the AppUserService class.
    /// </summary>
    /// <param name="appUserRepository">The AppUser repository instance.</param>
    /// <param name="mapConfig">The Mapster configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public AppUserService(IAppUserRepository appUserRepository, TypeAdapterConfig mapConfig,
        IConfiguration configuration)
    {
        _appUserRepository = appUserRepository;
        _mapConfig = mapConfig;

        // Configure Kafka producer
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
        };
        _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

        // Retrieve Kafka topic from configuration
        var topics = configuration.GetSection("KafkaSettings").GetSection("Topics").Get<string[]>();
        _kafkaTopic = topics != null && topics.Length > 0
            ? topics[0]
            : throw new ArgumentException("Kafka topic is not configured.");
    }

    /// <inheritdoc />
    public async Task<AppUserDto> GetByIdAsync(string id)
    {
        var appUser = await _appUserRepository.GetByIdAsync(id);
        if (appUser == null)
        {
            throw new KeyNotFoundException($"AppUser with ID {id} was not found.");
        }

        return appUser.Adapt<AppUserDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AppUserDto>> GetAllAsync()
    {
        var appUsers = await _appUserRepository.GetAllAsync();
        return appUsers.Adapt<IEnumerable<AppUserDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<AppUserDto> CreateAsync(CreateAppUserDto createDto)
    {
        // Map DTO to domain model
        var appUser = createDto.Adapt<AppUser>(_mapConfig);

        // TODO: Implement password hashing
        // appUser.Password = HashPassword(createDto.Password);

        // Set creation timestamp
        appUser.CreatedAt = DateTime.UtcNow;
        appUser.IsActive = true;
        appUser.IsVerified = false;

        // Insert the new user into the repository
        await _appUserRepository.CreateAsync(appUser);

        // Publish creation event to Kafka
        var message = appUser.Adapt<AppUserDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

        return appUser.Adapt<AppUserDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(string id, UpdateAppUserDto updateDto)
    {
        // Retrieve existing user
        var existingUser = await _appUserRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"AppUser with ID {id} was not found.");
        }

        // Map update DTO to existing user
        updateDto.Adapt(existingUser, _mapConfig);
        existingUser.UpdatedAt = DateTime.UtcNow;

        // Handle password update if provided
        if (!string.IsNullOrEmpty(updateDto.Password))
        {
            // TODO: Implement password hashing
            // existingUser.Password = HashPassword(updateDto.Password);
        }

        // Update the user in the repository
        await _appUserRepository.UpdateAsync(existingUser);

        // Publish update event to Kafka
        var message = existingUser.Adapt<AppUserDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        // Retrieve existing user
        var existingUser = await _appUserRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException($"AppUser with ID {id} was not found.");
        }

        // Delete the user from the repository
        await _appUserRepository.DeleteAsync(id);

        // Publish deletion event to Kafka
        var message = $"AppUser with ID {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}

