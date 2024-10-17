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
    /// Implements the ICalendarEventAttendeeService interface for CalendarEventAttendee business logic.
    /// </summary>
    public class CalendarEventAttendeeService : ICalendarEventAttendeeService
    {
        private readonly ICalendarEventAttendeeRepository _repository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the CalendarEventAttendeeService class.
        /// </summary>
        /// <param name="repository">The CalendarEventAttendee repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public CalendarEventAttendeeService(ICalendarEventAttendeeRepository repository, TypeAdapterConfig mapConfig,
            IConfiguration configuration)
        {
            _repository = repository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for ActivityService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("CalendarEventAttendeeTopic") 
                          ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
        }

        /// <inheritdoc />
        public async Task<CalendarEventAttendeeDto> GetByIdAsync(string id)
        {
            var attendee = await _repository.GetByIdAsync(id);
            if (attendee == null)
            {
                throw new KeyNotFoundException($"CalendarEventAttendee with ID {id} was not found.");
            }

            return attendee.Adapt<CalendarEventAttendeeDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CalendarEventAttendeeDto>> GetAllAsync()
        {
            var attendees = await _repository.GetAllAsync();
            return attendees.Adapt<IEnumerable<CalendarEventAttendeeDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<CalendarEventAttendeeDto> CreateAsync(CreateCalendarEventAttendeeDto createDto)
        {
            var attendee = createDto.Adapt<CalendarEventAttendee>(_mapConfig);
            attendee.CreatedAt = DateTime.UtcNow;
            attendee.IsActive = true;

            await _repository.CreateAsync(attendee);

            // Publish creation event to Kafka
            var message = attendee.Adapt<CalendarEventAttendeeDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return attendee.Adapt<CalendarEventAttendeeDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateCalendarEventAttendeeDto updateDto)
        {
            var existingAttendee = await _repository.GetByIdAsync(id);
            if (existingAttendee == null)
            {
                throw new KeyNotFoundException($"CalendarEventAttendee with ID {id} was not found.");
            }

            updateDto.Adapt(existingAttendee, _mapConfig);
            existingAttendee.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existingAttendee);

            // Publish update event to Kafka
            var message = existingAttendee.Adapt<CalendarEventAttendeeDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingAttendee = await _repository.GetByIdAsync(id);
            if (existingAttendee == null)
            {
                throw new KeyNotFoundException($"CalendarEventAttendee with ID {id} was not found.");
            }

            await _repository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"CalendarEventAttendee with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}