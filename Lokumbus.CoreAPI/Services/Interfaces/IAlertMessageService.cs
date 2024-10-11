using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for AlertMessage service operations.
    /// </summary>
    public interface IAlertMessageService
    {
        /// <summary>
        /// Retrieves an AlertMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AlertMessage.</param>
        /// <returns>The AlertMessageDto if found; otherwise, null.</returns>
        Task<AlertMessageDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all AlertMessages.
        /// </summary>
        /// <returns>A collection of all AlertMessageDtos.</returns>
        Task<IEnumerable<AlertMessageDto>> GetAllAsync();

        /// <summary>
        /// Creates a new AlertMessage.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created AlertMessageDto.</returns>
        Task<AlertMessageDto> CreateAsync(CreateAlertMessageDto createDto);

        /// <summary>
        /// Updates an existing AlertMessage.
        /// </summary>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(UpdateAlertMessageDto updateDto);

        /// <summary>
        /// Deletes an AlertMessage by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the AlertMessage to delete.</param>
        Task DeleteAsync(string id);
    }
}