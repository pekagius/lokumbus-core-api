using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Defines the contract for Location service operations.
/// </summary>
public interface ILocationService
{
    /// <summary>
    /// Retrieves all Locations.
    /// </summary>
    /// <returns>A list of Location DTOs.</returns>
    Task<IEnumerable<LocationDto>> GetAllLocationsAsync();

    /// <summary>
    /// Retrieves a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>The Location DTO if found; otherwise, null.</returns>
    Task<LocationDto?> GetLocationByIdAsync(string id);

    /// <summary>
    /// Creates a new Location.
    /// </summary>
    /// <param name="createDto">The DTO containing information to create the Location.</param>
    /// <returns>The created Location DTO.</returns>
    Task<LocationDto> CreateLocationAsync(CreateLocationDto createDto);

    /// <summary>
    /// Updates an existing Location.
    /// </summary>
    /// <param name="updateDto">The DTO containing updated information for the Location.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateLocationAsync(UpdateLocationDto updateDto);

    /// <summary>
    /// Deletes a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteLocationAsync(string id);
}