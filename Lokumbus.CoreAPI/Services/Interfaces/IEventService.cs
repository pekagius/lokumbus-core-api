using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Event service operations.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves an Event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Event.</param>
        /// <returns>The EventDto if found; otherwise, null.</returns>
        Task<EventDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Events.
        /// </summary>
        /// <returns>A collection of all EventDtos.</returns>
        Task<IEnumerable<EventDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created EventDto.</returns>
        Task<EventDto> CreateAsync(CreateEventDto createDto);

        /// <summary>
        /// Updates an existing Event.
        /// </summary>
        /// <param name="id">The unique identifier of the Event to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateEventDto updateDto);

        /// <summary>
        /// Deletes an Event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Event to delete.</param>
        Task DeleteAsync(string id);
    }
}