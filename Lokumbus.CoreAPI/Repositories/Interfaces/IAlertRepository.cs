using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Alert repository operations.
    /// </summary>
    public interface IAlertRepository
    {
        /// <summary>
        /// Retrieves an Alert by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert.</param>
        /// <returns>The Alert if found; otherwise, null.</returns>
        Task<Alert?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Alerts.
        /// </summary>
        /// <returns>A collection of all Alerts.</returns>
        Task<IEnumerable<Alert>> GetAllAsync();

        /// <summary>
        /// Creates a new Alert.
        /// </summary>
        /// <param name="alert">The Alert to create.</param>
        Task CreateAsync(Alert alert);

        /// <summary>
        /// Updates an existing Alert.
        /// </summary>
        /// <param name="alert">The Alert with updated information.</param>
        Task UpdateAsync(Alert alert);

        /// <summary>
        /// Deletes an Alert by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert to delete.</param>
        Task DeleteAsync(string id);
    }
}