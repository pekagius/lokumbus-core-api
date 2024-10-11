using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Alert service operations.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Retrieves an Alert by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert.</param>
        /// <returns>The AlertDto if found; otherwise, null.</returns>
        Task<AlertDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Alerts.
        /// </summary>
        /// <returns>A collection of all AlertDtos.</returns>
        Task<IEnumerable<AlertDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Alert.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created AlertDto.</returns>
        Task<AlertDto> CreateAsync(CreateAlertDto createDto);

        /// <summary>
        /// Updates an existing Alert.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateAlertDto updateDto);

        /// <summary>
        /// Deletes an Alert by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert to delete.</param>
        Task DeleteAsync(string id);
    }
}