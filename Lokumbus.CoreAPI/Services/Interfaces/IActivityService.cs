using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Activity service operations.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Retrieves an Activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity.</param>
        /// <returns>The ActivityDto if found; otherwise, null.</returns>
        Task<ActivityDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Activities.
        /// </summary>
        /// <returns>A collection of all ActivityDtos.</returns>
        Task<IEnumerable<ActivityDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Activity.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created ActivityDto.</returns>
        Task<ActivityDto> CreateAsync(CreateActivityDto createDto);

        /// <summary>
        /// Updates an existing Activity.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task<ActivityDto> UpdateAsync(string id, UpdateActivityDto updateDto);

        /// <summary>
        /// Deletes an Activity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity to delete.</param>
        Task DeleteAsync(string id);
    }
}