using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Interest service operations.
    /// </summary>
    public interface IInterestService
    {
        /// <summary>
        /// Retrieves an Interest by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest.</param>
        /// <returns>The InterestDto if found; otherwise, null.</returns>
        Task<InterestDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Interests.
        /// </summary>
        /// <returns>A collection of all InterestDtos.</returns>
        Task<IEnumerable<InterestDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Interest.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created InterestDto.</returns>
        Task<InterestDto> CreateAsync(CreateInterestDto createDto);

        /// <summary>
        /// Updates an existing Interest.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateInterestDto updateDto);

        /// <summary>
        /// Deletes an Interest by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest to delete.</param>
        Task DeleteAsync(string id);
    }
}