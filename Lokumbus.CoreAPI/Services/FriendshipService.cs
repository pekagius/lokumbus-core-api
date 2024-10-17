using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using Confluent.Kafka;
using Lokumbus.CoreAPI.Helpers;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implementiert das IFriendshipService-Interface für die Freundschafts-Business-Logik.
    /// </summary>
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
    
        /// <summary>
        /// Initialisiert eine neue Instanz der FriendshipService-Klasse.
        /// </summary>
        /// <param name="friendshipRepository">Das Friendship-Repository.</param>
        /// <param name="mapConfig">Die Mapster-Konfiguration.</param>
        /// <param name="configuration">Die Anwendungskonfiguration.</param>
        public FriendshipService(IFriendshipRepository friendshipRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _friendshipRepository = friendshipRepository;
            _mapConfig = mapConfig;
    
            // Kafka-Producer konfigurieren
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
    
            // Retrieve Kafka topic for ActivityService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("FriendshipTopic") 
                          ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
        }
    
        /// <inheritdoc />
        public async Task<FriendshipDto> GetByIdAsync(string id)
        {
            var friendship = await _friendshipRepository.GetByIdAsync(id);
            if (friendship == null)
            {
                throw new KeyNotFoundException($"Friendship mit ID {id} wurde nicht gefunden.");
            }
    
            return friendship.Adapt<FriendshipDto>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<FriendshipDto>> GetAllAsync()
        {
            var friendships = await _friendshipRepository.GetAllAsync();
            return friendships.Adapt<IEnumerable<FriendshipDto>>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<FriendshipDto>> GetByPersonaIdAsync(string personaId)
        {
            var friendships = await _friendshipRepository.GetByPersonaIdAsync(personaId);
            return friendships.Adapt<IEnumerable<FriendshipDto>>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task<FriendshipDto> CreateAsync(CreateFriendshipDto createDto)
        {
            // DTO zu Domain-Modell mappen
            var friendship = createDto.Adapt<Friendship>(_mapConfig);
    
            // Erstellungszeitpunkt und Standardwerte setzen
            friendship.CreatedAt = DateTime.UtcNow;
            friendship.UpdatedAt = DateTime.UtcNow;
            friendship.IsAccepted = false; // Standardmäßig nicht akzeptiert
    
            // Neue Friendship ins Repository einfügen
            await _friendshipRepository.CreateAsync(friendship);
    
            // Erstellungsevent zu Kafka publishen
            var message = friendship.Adapt<FriendshipDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    
            return friendship.Adapt<FriendshipDto>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateFriendshipDto updateDto)
        {
            // Bestehende Friendship abrufen
            var existingFriendship = await _friendshipRepository.GetByIdAsync(id);
            if (existingFriendship == null)
            {
                throw new KeyNotFoundException($"Friendship mit ID {id} wurde nicht gefunden.");
            }
    
            // Update-Daten auf bestehende Friendship anwenden
            updateDto.Adapt(existingFriendship, _mapConfig);
            existingFriendship.UpdatedAt = DateTime.UtcNow;
    
            // Freundschaft im Repository aktualisieren
            await _friendshipRepository.UpdateAsync(existingFriendship);
    
            // Update-Event zu Kafka publishen
            var message = existingFriendship.Adapt<FriendshipDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    
        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Bestehende Friendship abrufen
            var existingFriendship = await _friendshipRepository.GetByIdAsync(id);
            if (existingFriendship == null)
            {
                throw new KeyNotFoundException($"Friendship mit ID {id} wurde nicht gefunden.");
            }
    
            // Friendship aus dem Repository löschen
            await _friendshipRepository.DeleteAsync(id);
    
            // Lösch-Event zu Kafka publishen
            var message = $"Friendship mit ID {id} wurde gelöscht.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}