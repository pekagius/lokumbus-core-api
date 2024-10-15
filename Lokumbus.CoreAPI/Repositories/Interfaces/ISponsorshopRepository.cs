using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Sponsorship repository operations.
    /// </summary>
    public interface ISponsorshipRepository
    {
        /// <summary>
        /// Retrieves a Sponsorship by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship.</param>
        /// <returns>The Sponsorship if found; otherwise, null.</returns>
        Task<Sponsorship?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Sponsorships.
        /// </summary>
        /// <returns>A collection of all Sponsorships.</returns>
        Task<IEnumerable<Sponsorship>> GetAllAsync();

        /// <summary>
        /// Creates a new Sponsorship.
        /// </summary>
        /// <param name="sponsorship">The Sponsorship to create.</param>
        Task CreateAsync(Sponsorship sponsorship);

        /// <summary>
        /// Updates an existing Sponsorship.
        /// </summary>
        /// <param name="sponsorship">The Sponsorship with updated information.</param>
        Task UpdateAsync(Sponsorship sponsorship);

        /// <summary>
        /// Deletes a Sponsorship by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship to delete.</param>
        Task DeleteAsync(string id);
    }
}