using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

public interface ILocationRepository
{
    /// <summary>
    /// Retrieves all Locations.
    /// </summary>
    /// <returns>A list of Locations.</returns>
    Task<IEnumerable<Location>> GetAllAsync();

    /// <summary>
    /// Retrieves a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>The Location if found; otherwise, null.</returns>
    Task<Location?> GetByIdAsync(string id);

    /// <summary>
    /// Creates a new Location.
    /// </summary>
    /// <param name="location">The Location to create.</param>
    /// <returns>The created Location.</returns>
    Task<Location> CreateAsync(Location location);

    /// <summary>
    /// Updates an existing Location.
    /// </summary>
    /// <param name="location">The Location with updated information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(Location location);

    /// <summary>
    /// Deletes a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(string id);
}