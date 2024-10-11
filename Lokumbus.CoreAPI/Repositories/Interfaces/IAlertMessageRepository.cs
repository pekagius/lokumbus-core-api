using Lokumbus.CoreAPI.Models.SubClasses;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for AlertMessage repository operations.
    /// </summary>
    public interface IAlertMessageRepository
    {
        /// <summary>
        /// Retrieves an AlertMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AlertMessage.</param>
        /// <returns>The AlertMessage if found; otherwise, null.</returns>
        Task<AlertMessage> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all AlertMessages.
        /// </summary>
        /// <returns>A collection of all AlertMessages.</returns>
        Task<IEnumerable<AlertMessage>> GetAllAsync();

        /// <summary>
        /// Creates a new AlertMessage.
        /// </summary>
        /// <param name="alertMessage">The AlertMessage to create.</param>
        Task CreateAsync(AlertMessage alertMessage);

        /// <summary>
        /// Updates an existing AlertMessage.
        /// </summary>
        /// <param name="alertMessage">The AlertMessage with updated information.</param>
        Task UpdateAsync(AlertMessage alertMessage);

        /// <summary>
        /// Deletes an AlertMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AlertMessage to delete.</param>
        Task DeleteAsync(string id);
    }
}