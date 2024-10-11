using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for ChatMessage service operations.
    /// </summary>
    public interface IChatMessageService
    {
        /// <summary>
        /// Retrieves a ChatMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage.</param>
        /// <returns>The ChatMessageDto if found; otherwise, null.</returns>
        Task<ChatMessageDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all ChatMessages.
        /// </summary>
        /// <returns>A collection of all ChatMessageDtos.</returns>
        Task<IEnumerable<ChatMessageDto>> GetAllAsync();

        /// <summary>
        /// Creates a new ChatMessage.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created ChatMessageDto.</returns>
        Task<ChatMessageDto> CreateAsync(CreateChatMessageDto createDto);

        /// <summary>
        /// Updates an existing ChatMessage.
        /// </summary>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(UpdateChatMessageDto updateDto);

        /// <summary>
        /// Deletes a ChatMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage to delete.</param>
        Task DeleteAsync(string id);
    }
}