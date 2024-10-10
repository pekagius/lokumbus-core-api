using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Auth repository operations.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Retrieves an Auth entry by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry.</param>
        /// <returns>The Auth entry if found; otherwise, null.</returns>
        Task<Auth?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Auth entries.
        /// </summary>
        /// <returns>A collection of all Auth entries.</returns>
        Task<IEnumerable<Auth>> GetAllAsync();

        /// <summary>
        /// Creates a new Auth entry.
        /// </summary>
        /// <param name="auth">The Auth entry to create.</param>
        Task CreateAsync(Auth auth);

        /// <summary>
        /// Updates an existing Auth entry.
        /// </summary>
        /// <param name="auth">The Auth entry with updated information.</param>
        Task UpdateAsync(Auth auth);

        /// <summary>
        /// Deletes an Auth entry by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry to delete.</param>
        Task DeleteAsync(string id);
    }
}