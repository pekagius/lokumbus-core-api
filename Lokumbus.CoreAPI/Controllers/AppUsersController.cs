using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Lokumbus.CoreAPI.Controllers;

/// <summary>
/// Controller for managing AppUser resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppUsersController : ControllerBase
{
    private readonly IAppUserService _appUserService;

    /// <summary>
    /// Initializes a new instance of the AppUsersController class.
    /// </summary>
    /// <param name="appUserService">The AppUser service instance.</param>
    public AppUsersController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    /// <summary>
    /// Retrieves all AppUsers.
    /// </summary>
    /// <returns>A collection of AppUserDto.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetAll()
    {
        var appUsers = await _appUserService.GetAllAsync();
        return Ok(appUsers);
    }

    /// <summary>
    /// Retrieves a specific AppUser by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser.</param>
    /// <returns>The requested AppUserDto.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserDto>> GetById(string id)
    {
        try
        {
            var appUser = await _appUserService.GetByIdAsync(id);
            return Ok(appUser);
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the user does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new AppUser.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created AppUserDto.</returns>
    [HttpPost]
    public async Task<ActionResult<AppUserDto>> Create([FromBody] CreateAppUserDto createDto)
    {
        var createdAppUser = await _appUserService.CreateAsync(createDto);
        // Return 201 Created with location header
        return CreatedAtAction(nameof(GetById), new { id = createdAppUser.Id }, createdAppUser);
    }

    /// <summary>
    /// Updates an existing AppUser.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateAppUserDto updateDto)
    {
        try
        {
            await _appUserService.UpdateAsync(id, updateDto);
            // Return 204 No Content on successful update
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the user does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an AppUser by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the AppUser to delete.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _appUserService.DeleteAsync(id);
            // Return 204 No Content on successful deletion
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the user does not exist
            return NotFound(new { Message = ex.Message });
        }
    }
}