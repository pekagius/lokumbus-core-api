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
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly TypeAdapterConfig _mapConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageService"/> class.
        /// </summary>
        /// <param name="chatMessageRepository">The ChatMessage repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        public ChatMessageService(IChatMessageRepository chatMessageRepository, TypeAdapterConfig mapConfig)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapConfig = mapConfig;
        }

        /// <inheritdoc />
        public async Task<ChatMessageDto> GetByIdAsync(string id)
        {
            var chatMessage = await _chatMessageRepository.GetByIdAsync(id);
            if (chatMessage == null)
            {
                throw new KeyNotFoundException($"ChatMessage with ID {id} was not found.");
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
            // Map DTO to domain model
            var chatMessage = createDto.Adapt<ChatMessage>(_mapConfig);
            chatMessage.CreatedAt = DateTime.UtcNow;
            chatMessage.Status = MessageStatus.Sent;
            chatMessage.SentAt = DateTime.UtcNow;

            // Insert into repository
            await _chatMessageRepository.CreateAsync(chatMessage);

            return chatMessage.Adapt<ChatMessageDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UpdateChatMessageDto updateDto)
        {
            var existingMessage = await _chatMessageRepository.GetByIdAsync(updateDto.Id);
            if (existingMessage == null)
            {
                throw new KeyNotFoundException($"ChatMessage with ID {updateDto.Id} was not found.");
            }

            // Map update DTO to existing message
            updateDto.Adapt(existingMessage, _mapConfig);
            existingMessage.UpdatedAt = DateTime.UtcNow;

            await _chatMessageRepository.UpdateAsync(existingMessage);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingMessage = await _chatMessageRepository.GetByIdAsync(id);
            if (existingMessage == null)
            {
                throw new KeyNotFoundException($"ChatMessage with ID {id} was not found.");
            }

            await _chatMessageRepository.DeleteAsync(id);
        }
    }
}