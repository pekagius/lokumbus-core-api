using Lokumbus.CoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Event repository operations.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Retrieves an Event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Event.</param>
        /// <returns>The Event if found; otherwise, null.</returns>
        Task<Event> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Events.
        /// </summary>
        /// <returns>A collection of all Events.</returns>
        Task<IEnumerable<Event>> GetAllAsync();

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="event">The Event to create.</param>
        Task CreateAsync(Event @event);

        /// <summary>
        /// Updates an existing Event.
        /// </summary>
        /// <param name="event">The Event with updated information.</param>
        Task UpdateAsync(Event @event);

        /// <summary>
        /// Deletes an Event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Event to delete.</param>
        Task DeleteAsync(string id);
    }
}