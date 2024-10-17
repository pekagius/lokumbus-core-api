using System.Text.Json;
using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the <see cref="IAreaService"/> interface for Area business logic.
    /// </summary>
    public class AreaService : IAreaService, IDisposable
    {
        private readonly IAreaRepository _areaRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private readonly ILogger<AreaService> _logger;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaService"/> class.
        /// </summary>
        /// <param name="areaRepository">The Area repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="logger">The logger instance.</param>
        public AreaService(
            IAreaRepository areaRepository,
            TypeAdapterConfig mapConfig,
            IConfiguration configuration,
            ILogger<AreaService> logger)
        {
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
            _mapConfig = mapConfig ?? throw new ArgumentNullException(nameof(mapConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Konfiguration des Kafka-Producers
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Abrufen des Kafka-Topics für AreaService aus der Konfiguration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("AreaTopic")
                          ?? throw new ArgumentException("Kafka topic for AreaService is not configured.");
        }

        /// <inheritdoc />
        public async Task<AreaDto> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("GetByIdAsync called with empty ID.");
                throw new ArgumentException("ID darf nicht leer sein.", nameof(id));
            }

            var area = await _areaRepository.GetByIdAsync(id);
            if (area == null)
            {
                _logger.LogWarning($"Area mit ID {id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"Area mit ID {id} wurde nicht gefunden.");
            }

            _logger.LogInformation($"Area mit ID {id} erfolgreich abgerufen.");
            return area.Adapt<AreaDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AreaDto>> GetAllAsync()
        {
            var areas = await _areaRepository.GetAllAsync();
            _logger.LogInformation($"{areas?.Count() ?? 0} Areas erfolgreich abgerufen.");
            return areas.Adapt<IEnumerable<AreaDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AreaDto> CreateAsync(CreateAreaDto createAreaDto)
        {
            if (createAreaDto == null)
            {
                _logger.LogWarning("CreateAsync called with null CreateAreaDto.");
                throw new ArgumentNullException(nameof(createAreaDto));
            }

            // DTO auf Domain-Modell abbilden
            var area = createAreaDto.Adapt<Area>(_mapConfig);

            // Setzen der Erstellungszeit und Standardwerte
            area.CreatedAt = DateTime.UtcNow;

            // Einfügen in das Repository
            await _areaRepository.CreateAsync(area);
            _logger.LogInformation($"Area mit ID {area.Id} erfolgreich erstellt.");

            // DTO für Antwort und Kafka-Nachricht anpassen
            var areaDto = area.Adapt<AreaDto>(_mapConfig);

            // DTO zu JSON serialisieren
            var message = JsonSerializer.Serialize(areaDto);

            // Versand der Erstellungsevent-Nachricht an Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
                _logger.LogInformation($"Creation event für Area mit ID {areaDto.Id} an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden der Creation Event Nachricht für Area mit ID {areaDto.Id} an Kafka.");
                // Optional: Weitere Fehlerbehandlung, z.B. Retry-Mechanismen
                throw;
            }

            return areaDto;
        }

        /// <inheritdoc />
        public async Task<AreaDto> UpdateAsync(string id, UpdateAreaDto updateAreaDto)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("UpdateAsync called with empty ID.");
                throw new ArgumentException("ID darf nicht leer sein.", nameof(id));
            }

            if (updateAreaDto == null)
            {
                _logger.LogWarning("UpdateAsync called with null UpdateAreaDto.");
                throw new ArgumentNullException(nameof(updateAreaDto));
            }

            // Abrufen der bestehenden Area
            var existingArea = await _areaRepository.GetByIdAsync(id);
            if (existingArea == null)
            {
                _logger.LogWarning($"Area mit ID {id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"Area mit ID {id} wurde nicht gefunden.");
            }

            // Abbilden des Update-DTO auf die bestehende Area
            updateAreaDto.Adapt(existingArea, _mapConfig);
            existingArea.UpdatedAt = DateTime.UtcNow;

            // Aktualisieren im Repository
            await _areaRepository.UpdateAsync(existingArea);
            _logger.LogInformation($"Area mit ID {id} erfolgreich aktualisiert.");

            // DTO für Kafka-Nachricht anpassen
            var areaDto = existingArea.Adapt<AreaDto>(_mapConfig);

            // DTO zu JSON serialisieren
            var message = JsonSerializer.Serialize(areaDto);

            // Versand der Update-Event-Nachricht an Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
                _logger.LogInformation($"Update event für Area mit ID {id} an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden der Update Event Nachricht für Area mit ID {id} an Kafka.");
                // Optional: Weitere Fehlerbehandlung, z.B. Retry-Mechanismen
                throw;
            }

            return areaDto;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("DeleteAsync called with empty ID.");
                throw new ArgumentException("ID darf nicht leer sein.", nameof(id));
            }

            // Abrufen der bestehenden Area
            var existingArea = await _areaRepository.GetByIdAsync(id);
            if (existingArea == null)
            {
                _logger.LogWarning($"Area mit ID {id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"Area mit ID {id} wurde nicht gefunden.");
            }

            // Löschen aus dem Repository
            await _areaRepository.DeleteAsync(id);
            _logger.LogInformation($"Area mit ID {id} erfolgreich gelöscht.");

            // Erstellung einer Deletion-Event-Nachricht
            var deletionMessage = new
            {
                Event = "AreaDeleted",
                AreaId = id,
                Timestamp = DateTime.UtcNow
            };
            var messageJson = JsonSerializer.Serialize(deletionMessage);

            // Versand der Deletion-Event-Nachricht an Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = messageJson });
                _logger.LogInformation($"Deletion event für Area mit ID {id} an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden der Deletion Event Nachricht für Area mit ID {id} an Kafka.");
                // Optional: Weitere Fehlerbehandlung, z.B. Retry-Mechanismen
                throw;
            }
        }

        /// <summary>
        /// Disposes the Kafka producer and releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Sicherstellen, dass alle Nachrichten gesendet werden, bevor der Producer geschlossen wird
                    _kafkaProducer?.Flush(TimeSpan.FromSeconds(10));
                    _kafkaProducer?.Dispose();
                    _logger.LogInformation("Kafka Producer wurde ordnungsgemäß freigegeben.");
                }

                _disposed = true;
            }
        }
    }
}