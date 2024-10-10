using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

/// <summary>
/// Defines the contract for AppUser repository operations.
/// </summary>
public interface IAppUserRepository
{
    /// <summary>
    /// Retrieves an AppUser by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser.</param>
    /// <returns>The AppUser if found; otherwise, null.</returns>
    Task<AppUser> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all AppUsers.
    /// </summary>
    /// <returns>A collection of all AppUsers.</returns>
    Task<IEnumerable<AppUser>> GetAllAsync();

    /// <summary>
    /// Creates a new AppUser.
    /// </summary>
    /// <param name="appUser">The AppUser to create.</param>
    Task CreateAsync(AppUser appUser);

    /// <summary>
    /// Updates an existing AppUser.
    /// </summary>
    /// <param name="appUser">The AppUser with updated information.</param>
    Task UpdateAsync(AppUser appUser);

    /// <summary>
    /// Deletes an AppUser by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser to delete.</param>
    Task DeleteAsync(string id);
}