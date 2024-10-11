using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Organizer repository operations.
    /// </summary>
    public interface IOrganizerRepository
    {
        /// <summary>
        /// Retrieves an Organizer by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer.</param>
        /// <returns>The Organizer if found; otherwise, null.</returns>
        Task<Organizer> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Organizers.
        /// </summary>
        /// <returns>A collection of all Organizers.</returns>
        Task<IEnumerable<Organizer>> GetAllAsync();

        /// <summary>
        /// Creates a new Organizer.
        /// </summary>
        /// <param name="organizer">The Organizer to create.</param>
        Task CreateAsync(Organizer organizer);

        /// <summary>
        /// Updates an existing Organizer.
        /// </summary>
        /// <param name="organizer">The Organizer with updated information.</param>
        Task UpdateAsync(Organizer organizer);

        /// <summary>
        /// Deletes an Organizer by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer to delete.</param>
        Task DeleteAsync(string id);
    }
}