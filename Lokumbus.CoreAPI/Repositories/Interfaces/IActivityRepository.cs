using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Activity repository operations.
    /// </summary>
    public interface IActivityRepository
    {
        /// <summary>
        /// Retrieves an Activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity.</param>
        /// <returns>The Activity if found; otherwise, null.</returns>
        Task<Activity> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Activities.
        /// </summary>
        /// <returns>A collection of all Activities.</returns>
        Task<IEnumerable<Activity>> GetAllAsync();

        /// <summary>
        /// Creates a new Activity.
        /// </summary>
        /// <param name="activity">The Activity to create.</param>
        Task CreateAsync(Activity activity);

        /// <summary>
        /// Updates an existing Activity.
        /// </summary>
        /// <param name="activity">The Activity with updated information.</param>
        Task UpdateAsync(Activity activity);

        /// <summary>
        /// Deletes an Activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity to delete.</param>
        Task DeleteAsync(string id);
    }
}