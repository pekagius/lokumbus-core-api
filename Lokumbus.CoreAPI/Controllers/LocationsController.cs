using System.Collections.Generic;
using System.Threading.Tasks;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationController"/> class.
    /// </summary>
    /// <param name="locationService">The Location service.</param>
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Retrieves all Locations.
    /// </summary>
    /// <returns>A list of Location DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
    {
        var locations = await _locationService.GetAllLocationsAsync();
        return Ok(locations);
    }

    /// <summary>
    /// Retrieves a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>The Location DTO if found; otherwise, NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetLocationById(string id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(location);
    }

    /// <summary>
    /// Creates a new Location.
    /// </summary>
    /// <param name="createDto">The DTO containing information to create the Location.</param>
    /// <returns>The created Location DTO.</returns>
    [HttpPost]
    public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] CreateLocationDto createDto)
    {
        var createdLocation = await _locationService.CreateLocationAsync(createDto);
        return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.Id }, createdLocation);
    }

    /// <summary>
    /// Updates an existing Location.
    /// </summary>
    /// <param name="updateDto">The DTO containing updated information for the Location.</param>
    /// <returns>NoContent if successful; otherwise, appropriate error response.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationDto updateDto)
    {
        await _locationService.UpdateLocationAsync(updateDto);
        return NoContent();
    }

    /// <summary>
    /// Deletes a Location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Location.</param>
    /// <returns>NoContent if successful; otherwise, appropriate error response.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(string id)
    {
        await _locationService.DeleteLocationAsync(id);
        return NoContent();
    }
}