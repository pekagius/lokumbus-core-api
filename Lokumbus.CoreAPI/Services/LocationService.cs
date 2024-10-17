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
/// Implements the <see cref="ILocationService"/> interface for Location business logic.
/// </summary>
public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationService"/> class.
    /// </summary>
    /// <param name="locationRepository">The Location repository instance.</param>
    /// <param name="mapConfig">The Mapster configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public LocationService(ILocationRepository locationRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
    {
        _locationRepository = locationRepository;
        _mapConfig = mapConfig;

        // Configure Kafka producer
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
        };
        _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

        // Retrieve Kafka topic for ActivityService from configuration
        _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("LocationTopic") 
                      ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
    {
        var locations = await _locationRepository.GetAllAsync();
        return locations.Adapt<IEnumerable<LocationDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<LocationDto?> GetLocationByIdAsync(string id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        return location?.Adapt<LocationDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<LocationDto> CreateLocationAsync(CreateLocationDto createDto)
    {
        // Map DTO to domain model
        var location = createDto.Adapt<Location>(_mapConfig);

        // Insert the new location into the repository
        var createdLocation = await _locationRepository.CreateAsync(location);

        // Publish creation event to Kafka
        var locationDto = createdLocation.Adapt<LocationDto>(_mapConfig);
        var message = locationDto.Adapt<string>(_mapConfig); // Assuming an extension method ToJson exists
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = locationDto.ToJson() });

        return locationDto;
    }

    /// <inheritdoc />
    public async Task UpdateLocationAsync(UpdateLocationDto updateDto)
    {
        // Retrieve existing location
        var existingLocation = await _locationRepository.GetByIdAsync(updateDto.Id);
        if (existingLocation == null)
        {
            throw new KeyNotFoundException($"Location with Id {updateDto.Id} not found.");
        }

        // Map update DTO to existing location
        updateDto.Adapt(existingLocation, _mapConfig);
        existingLocation.UpdatedAt = DateTime.UtcNow;

        // Update the location in the repository
        await _locationRepository.UpdateAsync(existingLocation);

        // Publish update event to Kafka
        var locationDto = existingLocation.Adapt<LocationDto>(_mapConfig);
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = locationDto.ToJson() });
    }

    /// <inheritdoc />
    public async Task DeleteLocationAsync(string id)
    {
        // Retrieve existing location
        var existingLocation = await _locationRepository.GetByIdAsync(id);
        if (existingLocation == null)
        {
            throw new KeyNotFoundException($"Location with Id {id} not found.");
        }

        // Delete the location from the repository
        await _locationRepository.DeleteAsync(id);

        // Publish deletion event to Kafka
        var message = $"Location with Id {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}