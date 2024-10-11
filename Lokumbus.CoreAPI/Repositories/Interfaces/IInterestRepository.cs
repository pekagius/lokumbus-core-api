using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Interest repository operations.
    /// </summary>
    public interface IInterestRepository
    {
        /// <summary>
        /// Retrieves an Interest by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest.</param>
        /// <returns>The Interest if found; otherwise, null.</returns>
        Task<Interest?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Interests.
        /// </summary>
        /// <returns>A collection of all Interests.</returns>
        Task<IEnumerable<Interest>> GetAllAsync();

        /// <summary>
        /// Creates a new Interest.
        /// </summary>
        /// <param name="interest">The Interest to create.</param>
        Task CreateAsync(Interest interest);

        /// <summary>
        /// Updates an existing Interest.
        /// </summary>
        /// <param name="interest">The Interest with updated information.</param>
        Task UpdateAsync(Interest interest);

        /// <summary>
        /// Deletes an Interest by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest to delete.</param>
        Task DeleteAsync(string id);
    }
}