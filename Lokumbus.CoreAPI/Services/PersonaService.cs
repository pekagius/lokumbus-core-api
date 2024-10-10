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
/// Implements the IPersonaService interface for Persona business logic.
/// </summary>
public class PersonaService : IPersonaService
{
    private readonly IPersonaRepository _personaRepository;
    private readonly TypeAdapterConfig _mapConfig;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly string _kafkaTopic;

    /// <summary>
    /// Initializes a new instance of the PersonaService class.
    /// </summary>
    /// <param name="personaRepository">The Persona repository instance.</param>
    /// <param name="mapConfig">The Mapster configuration.</param>
    /// <param name="configuration">The application configuration.</param>
    public PersonaService(IPersonaRepository personaRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
    {
        _personaRepository = personaRepository;
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
    public async Task<PersonaDto> GetByIdAsync(string id)
    {
        var persona = await _personaRepository.GetByIdAsync(id);
        if (persona == null)
        {
            throw new KeyNotFoundException($"Persona with ID {id} was not found.");
        }

        return persona.Adapt<PersonaDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PersonaDto>> GetByUserIdAsync(string userId)
    {
        var personas = await _personaRepository.GetByUserIdAsync(userId);
        return personas.Adapt<IEnumerable<PersonaDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PersonaDto>> GetAllAsync()
    {
        var personas = await _personaRepository.GetAllAsync();
        return personas.Adapt<IEnumerable<PersonaDto>>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task<PersonaDto> CreateAsync(CreatePersonaDto createDto)
    {
        // Map DTO to domain model
        var persona = createDto.Adapt<Persona>(_mapConfig);

        // Set creation timestamp and default values
        persona.CreatedAt = DateTime.UtcNow;
        persona.IsActive = true;

        // Insert the new persona into the repository
        await _personaRepository.CreateAsync(persona);

        // Publish creation event to Kafka
        var message = persona.Adapt<PersonaDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

        return persona.Adapt<PersonaDto>(_mapConfig);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(string id, UpdatePersonaDto updateDto)
    {
        // Retrieve existing persona
        var existingPersona = await _personaRepository.GetByIdAsync(id);
        if (existingPersona == null)
        {
            throw new KeyNotFoundException($"Persona with ID {id} was not found.");
        }

        // Map update DTO to existing persona
        updateDto.Adapt(existingPersona, _mapConfig);
        existingPersona.UpdatedAt = DateTime.UtcNow;

        // Update the persona in the repository
        await _personaRepository.UpdateAsync(existingPersona);

        // Publish update event to Kafka
        var message = existingPersona.Adapt<PersonaDto>(_mapConfig).ToJson();
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        // Retrieve existing persona
        var existingPersona = await _personaRepository.GetByIdAsync(id);
        if (existingPersona == null)
        {
            throw new KeyNotFoundException($"Persona with ID {id} was not found.");
        }

        // Delete the persona from the repository
        await _personaRepository.DeleteAsync(id);

        // Publish deletion event to Kafka
        var message = $"Persona with ID {id} has been deleted.";
        await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    }
}
