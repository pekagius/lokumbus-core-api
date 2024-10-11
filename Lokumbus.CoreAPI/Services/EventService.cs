using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the IEventService interface for Event business logic.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the EventService class.
        /// </summary>
        /// <param name="eventRepository">The Event repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public EventService(IEventRepository eventRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _eventRepository = eventRepository;
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
        public async Task<EventDto> GetByIdAsync(string id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null)
            {
                throw new KeyNotFoundException($"Event with ID {id} was not found.");
            }

            return @event.Adapt<EventDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Adapt<IEnumerable<EventDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<EventDto> CreateAsync(CreateEventDto createDto)
        {
            // Map DTO to domain model
            var @event = createDto.Adapt<Event>(_mapConfig);

            // Set creation timestamp and default values
            @event.CreatedAt = DateTime.UtcNow;
            @event.IsActive = true;

            // Insert the new event into the repository
            await _eventRepository.CreateAsync(@event);

            // Publish creation event to Kafka
            var message = @event.Adapt<EventDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return @event.Adapt<EventDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateEventDto updateDto)
        {
            // Retrieve existing event
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException($"Event with ID {id} was not found.");
            }

            // Map update DTO to existing event
            updateDto.Adapt(existingEvent, _mapConfig);
            existingEvent.UpdatedAt = DateTime.UtcNow;

            // Update the event in the repository
            await _eventRepository.UpdateAsync(existingEvent);

            // Publish update event to Kafka
            var message = existingEvent.Adapt<EventDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing event
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException($"Event with ID {id} was not found.");
            }

            // Delete the event from the repository
            await _eventRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Event with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}