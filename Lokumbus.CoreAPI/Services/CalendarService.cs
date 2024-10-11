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
    /// Implementiert das ICalendarService-Interface für die Geschäftslogik rund um Kalender.
    /// </summary>
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
    
        /// <summary>
        /// Initialisiert eine neue Instanz der CalendarService-Klasse.
        /// </summary>
        /// <param name="calendarRepository">Das Kalender-Repository.</param>
        /// <param name="mapConfig">Die Mapster-Konfiguration.</param>
        /// <param name="configuration">Die Anwendungs-Konfiguration.</param>
        public CalendarService(ICalendarRepository calendarRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _calendarRepository = calendarRepository;
            _mapConfig = mapConfig;
    
            // Konfiguriere Kafka Producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
    
            // Hole Kafka Topic aus Konfiguration
            var topics = configuration.GetSection("KafkaSettings:Topics").Get<string[]>();
            _kafkaTopic = topics != null && topics.Length > 0 ? topics[0] : throw new ArgumentException("Kafka Topic ist nicht konfiguriert.");
        }
    
        /// <inheritdoc />
        public async Task<CalendarDto> GetByIdAsync(string id)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            if (calendar == null)
            {
                throw new KeyNotFoundException($"Calendar mit ID {id} wurde nicht gefunden.");
            }
    
            return calendar.Adapt<CalendarDto>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task<IEnumerable<CalendarDto>> GetAllAsync()
        {
            var calendars = await _calendarRepository.GetAllAsync();
            return calendars.Adapt<IEnumerable<CalendarDto>>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task<CalendarDto> CreateAsync(CreateCalendarDto createDto)
        {
            // Mappe DTO zum Domain-Modell
            var calendar = createDto.Adapt<Calendar>(_mapConfig);
    
            // Setze Erstellungszeitpunkt und Standardwerte
            calendar.CreatedAt = DateTime.UtcNow;
            calendar.UpdatedAt = DateTime.UtcNow;
    
            // Füge den neuen Kalender zum Repository hinzu
            await _calendarRepository.CreateAsync(calendar);
    
            // Publiziere ein Erstellungsereignis zu Kafka
            var message = calendar.Adapt<CalendarDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
    
            return calendar.Adapt<CalendarDto>(_mapConfig);
        }
    
        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateCalendarDto updateDto)
        {
            // Hole bestehenden Kalender
            var existingCalendar = await _calendarRepository.GetByIdAsync(id);
            if (existingCalendar == null)
            {
                throw new KeyNotFoundException($"Calendar mit ID {id} wurde nicht gefunden.");
            }
    
            // Mappe Update-DTO auf bestehenden Kalender
            updateDto.Adapt(existingCalendar, _mapConfig);
            existingCalendar.UpdatedAt = DateTime.UtcNow;
    
            // Aktualisiere den Kalender im Repository
            await _calendarRepository.UpdateAsync(existingCalendar);
    
            // Publiziere ein Update-Ereignis zu Kafka
            var message = existingCalendar.Adapt<CalendarDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    
        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Hole bestehenden Kalender
            var existingCalendar = await _calendarRepository.GetByIdAsync(id);
            if (existingCalendar == null)
            {
                throw new KeyNotFoundException($"Calendar mit ID {id} wurde nicht gefunden.");
            }
    
            // Lösche den Kalender aus dem Repository
            await _calendarRepository.DeleteAsync(id);
    
            // Publiziere ein Löschereignis zu Kafka
            var message = $"Calendar mit ID {id} wurde gelöscht.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}