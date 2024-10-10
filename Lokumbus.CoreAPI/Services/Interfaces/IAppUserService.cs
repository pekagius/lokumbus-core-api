using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Defines the contract for AppUser service operations.
/// </summary>
public interface IAppUserService
{
    /// <summary>
    /// Retrieves an AppUser by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser.</param>
    /// <returns>The AppUserDto if found; otherwise, null.</returns>
    Task<AppUserDto> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all AppUsers.
    /// </summary>
    /// <returns>A collection of all AppUserDtos.</returns>
    Task<IEnumerable<AppUserDto>> GetAllAsync();

    /// <summary>
    /// Creates a new AppUser.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created AppUserDto.</returns>
    Task<AppUserDto> CreateAsync(CreateAppUserDto createDto);

    /// <summary>
    /// Updates an existing AppUser.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    Task UpdateAsync(string id, UpdateAppUserDto updateDto);

    /// <summary>
    /// Deletes an AppUser by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser to delete.</param>
    Task DeleteAsync(string id);
}