using System.Collections.Generic;
using System.Threading.Tasks;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Auth service operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Retrieves an Auth entry by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry.</param>
        /// <returns>The AuthDto if found; otherwise, null.</returns>
        Task<AuthDto?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Auth entries.
        /// </summary>
        /// <returns>A collection of all AuthDtos.</returns>
        Task<IEnumerable<AuthDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Auth entry.
        /// </summary>
        /// <param name="createAuthDto">The DTO containing creation data.</param>
        /// <returns>The created AuthDto.</returns>
        Task<AuthDto> CreateAsync(CreateAuthDto createAuthDto);

        /// <summary>
        /// Updates an existing Auth entry.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry to update.</param>
        /// <param name="updateAuthDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateAuthDto updateAuthDto);

        /// <summary>
        /// Deletes an Auth entry by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry to delete.</param>
        Task DeleteAsync(string id);
    }
}