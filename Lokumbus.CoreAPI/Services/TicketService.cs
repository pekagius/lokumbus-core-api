using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the ITicketService interface for Ticket business logic.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the TicketService class.
        /// </summary>
        /// <param name="ticketRepository">The Ticket repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public TicketService(ITicketRepository ticketRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _ticketRepository = ticketRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic from configuration
            var topics = configuration.GetSection("KafkaSettings:Topics").Get<string[]>();
            _kafkaTopic = topics != null && topics.Length > 0 ? topics[0] : throw new ArgumentException("Kafka topic is not configured.");
        }

        /// <inheritdoc />
        public async Task<TicketDto> GetByIdAsync(string id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} was not found.");
            }

            return ticket.Adapt<TicketDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TicketDto>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets.Adapt<IEnumerable<TicketDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<TicketDto> CreateAsync(CreateTicketDto createDto)
        {
            // Map DTO to domain model
            var ticket = createDto.Adapt<Ticket>(_mapConfig);

            // Set creation timestamp and default values
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.IsActive = true;

            // Insert the new ticket into the repository
            await _ticketRepository.CreateAsync(ticket);

            // Publish creation event to Kafka
            var message = ticket.Adapt<TicketDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return ticket.Adapt<TicketDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateTicketDto updateDto)
        {
            // Retrieve existing ticket
            var existingTicket = await _ticketRepository.GetByIdAsync(id);
            if (existingTicket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} was not found.");
            }

            // Map update DTO to existing ticket
            updateDto.Adapt(existingTicket, _mapConfig);
            existingTicket.UpdatedAt = DateTime.UtcNow;

            // Update the ticket in the repository
            await _ticketRepository.UpdateAsync(existingTicket);

            // Publish update event to Kafka
            var message = existingTicket.Adapt<TicketDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            // Retrieve existing ticket
            var existingTicket = await _ticketRepository.GetByIdAsync(id);
            if (existingTicket == null)
            {
                throw new KeyNotFoundException($"Ticket with ID {id} was not found.");
            }

            // Delete the ticket from the repository
            await _ticketRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Ticket with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}