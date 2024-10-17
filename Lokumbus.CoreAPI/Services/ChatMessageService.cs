using System.Text.Json;
using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;


namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the <see cref="IChatMessageService"/> interface for ChatMessage business logic.
    /// </summary>
    public class ChatMessageService : IChatMessageService, IDisposable
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private readonly ILogger<ChatMessageService> _logger;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageService"/> class.
        /// </summary>
        /// <param name="chatMessageRepository">The ChatMessage repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="logger">The logger instance.</param>
        public ChatMessageService(
            IChatMessageRepository chatMessageRepository,
            TypeAdapterConfig mapConfig,
            IConfiguration configuration,
            ILogger<ChatMessageService> logger)
        {
            _chatMessageRepository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
            _mapConfig = mapConfig ?? throw new ArgumentNullException(nameof(mapConfig));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for ChatMessageService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("ChatMessageTopic")
                          ?? throw new ArgumentException("Kafka topic for ChatMessageService is not configured.");
        }

        /// <inheritdoc />
        public async Task<ChatMessageDto> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID darf nicht leer sein.", nameof(id));

            var chatMessage = await _chatMessageRepository.GetByIdAsync(id);
            if (chatMessage == null)
            {
                _logger.LogWarning($"ChatMessage mit ID {id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"ChatMessage mit ID {id} wurde nicht gefunden.");
            }

            return chatMessage.Adapt<ChatMessageDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ChatMessageDto>> GetAllAsync()
        {
            var chatMessages = await _chatMessageRepository.GetAllAsync();
            return chatMessages.Adapt<IEnumerable<ChatMessageDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<ChatMessageDto> CreateAsync(CreateChatMessageDto createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException(nameof(createDto));

            // Map DTO to domain model
            var chatMessage = createDto.Adapt<ChatMessage>(_mapConfig);

            // Set creation timestamp and default values
            chatMessage.CreatedAt = DateTime.UtcNow;
            chatMessage.Status = MessageStatus.Sent;
            chatMessage.SentAt = DateTime.UtcNow;

            // Insert into repository
            await _chatMessageRepository.CreateAsync(chatMessage);

            // Adapt to DTO for response and Kafka message
            var chatMessageDto = chatMessage.Adapt<ChatMessageDto>(_mapConfig);

            // Serialize DTO to JSON
            var message = JsonSerializer.Serialize(chatMessageDto);

            // Publish creation event to Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
                _logger.LogInformation($"ChatMessage mit ID {chatMessageDto.Id} wurde erstellt und an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden der ChatMessage mit ID {chatMessageDto.Id} an Kafka.");
                // Optional: Handle the exception as needed, e.g., retry, compensate, etc.
                throw;
            }

            return chatMessageDto;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UpdateChatMessageDto updateDto)
        {
            if (updateDto == null)
                throw new ArgumentNullException(nameof(updateDto));

            var existingMessage = await _chatMessageRepository.GetByIdAsync(updateDto.Id);
            if (existingMessage == null)
            {
                _logger.LogWarning($"ChatMessage mit ID {updateDto.Id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"ChatMessage mit ID {updateDto.Id} wurde nicht gefunden.");
            }

            // Map update DTO to existing message
            updateDto.Adapt(existingMessage, _mapConfig);
            existingMessage.UpdatedAt = DateTime.UtcNow;

            // Update in repository
            await _chatMessageRepository.UpdateAsync(existingMessage);

            // Adapt to DTO for Kafka message
            var chatMessageDto = existingMessage.Adapt<ChatMessageDto>(_mapConfig);

            // Serialize DTO to JSON
            var message = JsonSerializer.Serialize(chatMessageDto);

            // Publish update event to Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
                _logger.LogInformation($"ChatMessage mit ID {chatMessageDto.Id} wurde aktualisiert und an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden der aktualisierten ChatMessage mit ID {chatMessageDto.Id} an Kafka.");
                // Optional: Handle the exception as needed, e.g., retry, compensate, etc.
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID darf nicht leer sein.", nameof(id));

            var existingMessage = await _chatMessageRepository.GetByIdAsync(id);
            if (existingMessage == null)
            {
                _logger.LogWarning($"ChatMessage mit ID {id} wurde nicht gefunden.");
                throw new KeyNotFoundException($"ChatMessage mit ID {id} wurde nicht gefunden.");
            }

            // Delete from repository
            await _chatMessageRepository.DeleteAsync(id);

            // Create a deletion event message
            var deletionMessage = new
            {
                Event = "ChatMessageDeleted",
                ChatMessageId = id,
                Timestamp = DateTime.UtcNow
            };
            var messageJson = JsonSerializer.Serialize(deletionMessage);

            // Publish deletion event to Kafka
            try
            {
                await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = messageJson });
                _logger.LogInformation($"ChatMessage mit ID {id} wurde gelöscht und ein Deletion Event an Kafka Topic '{_kafkaTopic}' gesendet.");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, $"Fehler beim Senden des Deletion Events für ChatMessage mit ID {id} an Kafka.");
                // Optional: Handle the exception as needed, e.g., retry, compensate, etc.
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
                    // Ensure all messages are sent before disposing
                    _kafkaProducer?.Flush(TimeSpan.FromSeconds(10));
                    _kafkaProducer?.Dispose();
                    _logger.LogInformation("Kafka Producer wurde ordnungsgemäß freigegeben.");
                }

                _disposed = true;
            }
        }
    }
}