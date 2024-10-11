using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Organizer service operations.
    /// </summary>
    public interface IOrganizerService
    {
        /// <summary>
        /// Retrieves an Organizer by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer.</param>
        /// <returns>The OrganizerDto if found; otherwise, null.</returns>
        Task<OrganizerDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Organizers.
        /// </summary>
        /// <returns>A collection of all OrganizerDtos.</returns>
        Task<IEnumerable<OrganizerDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Organizer.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created OrganizerDto.</returns>
        Task<OrganizerDto> CreateAsync(CreateOrganizerDto createDto);

        /// <summary>
        /// Updates an existing Organizer.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateOrganizerDto updateDto);

        /// <summary>
        /// Deletes an Organizer by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer to delete.</param>
        Task DeleteAsync(string id);
    }
}