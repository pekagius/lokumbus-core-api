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
    /// Implementiert das IInviteService für die Geschäftslogik von Invite.
    /// </summary>
    public class InviteService : IInviteService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initialisiert eine neue Instanz der InviteService Klasse.
        /// </summary>
        /// <param name="inviteRepository">Das InviteRepository.</param>
        /// <param name="mapConfig">Die Mapster-Konfiguration.</param>
        /// <param name="configuration">Die Anwendungskonfiguration.</param>
        public InviteService(IInviteRepository inviteRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _inviteRepository = inviteRepository;
            _mapConfig = mapConfig;

            // Konfigurieren des Kafka Producers
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Abrufen des Kafka-Themas aus der Konfiguration
            var topics = configuration.GetSection("KafkaSettings").GetSection("Topics").Get<string[]>();
            _kafkaTopic = topics != null && topics.Length > 0
                ? topics[0]
                : throw new ArgumentException("Kafka-Topic ist nicht konfiguriert.");
        }

        /// <inheritdoc />
        public async Task<InviteDto?> GetByIdAsync(string id)
        {
            var invite = await _inviteRepository.GetByIdAsync(id);
            return invite?.Adapt<InviteDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InviteDto>> GetAllAsync()
        {
            var invites = await _inviteRepository.GetAllAsync();
            return invites.Adapt<IEnumerable<InviteDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<InviteDto> CreateAsync(CreateInviteDto createDto)
        {
            // DTO zu Domain-Modell abbilden
            var invite = createDto.Adapt<Invite>(_mapConfig);

            // Setzen von Erstellungszeitpunkt und Standardwerten
            invite.CreatedAt = DateTime.UtcNow;
            invite.IsActive = true;

            // Einfügen der neuen Einladung in das Repository
            await _inviteRepository.CreateAsync(invite);

            // Veröffentlichung des Erstellungsevents an Kafka
            var message = invite.Adapt<InviteDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return invite.Adapt<InviteDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateInviteDto updateDto)
        {
            // Bestehende Einladung abrufen
            var existingInvite = await _inviteRepository.GetByIdAsync(id);
            if (existingInvite == null)
            {
                throw new KeyNotFoundException($"Invite mit ID {id} wurde nicht gefunden.");
            }

            // Update DTO auf bestehende Einladung anwenden
            updateDto.Adapt(existingInvite, _mapConfig);
            existingInvite.UpdatedAt = DateTime.UtcNow;

            // Aktualisieren der Einladung im Repository
            await _inviteRepository.UpdateAsync(existingInvite);

            // Veröffentlichung des Aktualisierungsevents an Kafka
            var message = existingInvite.Adapt<InviteDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Bestehende Einladung abrufen
            var existingInvite = await _inviteRepository.GetByIdAsync(id);
            if (existingInvite == null)
            {
                throw new KeyNotFoundException($"Invite mit ID {id} wurde nicht gefunden.");
            }

            // Löschen der Einladung aus dem Repository
            await _inviteRepository.DeleteAsync(id);

            // Veröffentlichung des Löschungsereignisses an Kafka
            var message = $"Invite mit ID {id} wurde gelöscht.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}