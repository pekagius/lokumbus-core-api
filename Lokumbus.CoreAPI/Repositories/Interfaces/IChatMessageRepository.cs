using Lokumbus.CoreAPI.Models.SubClasses;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for ChatMessage repository operations.
    /// </summary>
    public interface IChatMessageRepository
    {
        /// <summary>
        /// Retrieves a ChatMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage.</param>
        /// <returns>The ChatMessage if found; otherwise, null.</returns>
        Task<ChatMessage> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all ChatMessages.
        /// </summary>
        /// <returns>A collection of all ChatMessages.</returns>
        Task<IEnumerable<ChatMessage>> GetAllAsync();

        /// <summary>
        /// Creates a new ChatMessage.
        /// </summary>
        /// <param name="chatMessage">The ChatMessage to create.</param>
        Task CreateAsync(ChatMessage chatMessage);

        /// <summary>
        /// Updates an existing ChatMessage.
        /// </summary>
        /// <param name="chatMessage">The ChatMessage with updated information.</param>
        Task UpdateAsync(ChatMessage chatMessage);

        /// <summary>
        /// Deletes a ChatMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage to delete.</param>
        Task DeleteAsync(string id);
    }
}