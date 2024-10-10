using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreaController : ControllerBase
{
    private readonly IAreaService _areaService;

    public AreaController(IAreaService areaService)
    {
        _areaService = areaService;
    }

    /// <summary>
    /// Get an Area by its ID.
    /// </summary>
    /// <param name="id">The ID of the Area.</param>
    /// <returns>The Area with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AreaDto>> GetById(string id)
    {
        var area = await _areaService.GetByIdAsync(id);
        return Ok(area);
    }

    /// <summary>
    /// Get all Areas.
    /// </summary>
    /// <returns>A list of all Areas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AreaDto>>> GetAll()
    {
        var areas = await _areaService.GetAllAsync();
        return Ok(areas);
    }

    /// <summary>
    /// Create a new Area.
    /// </summary>
    /// <param name="createAreaDto">The data for the new Area.</param>
    /// <returns>The created Area.</returns>
    [HttpPost]
    public async Task<ActionResult<AreaDto>> Create(CreateAreaDto createAreaDto)
    {
        var area = await _areaService.CreateAsync(createAreaDto);
        return CreatedAtAction(nameof(GetById), new { id = area.Id }, area);
    }

    /// <summary>
    /// Update an existing Area.
    /// </summary>
    /// <param name="id">The ID of the Area to update.</param>
    /// <param name="updateAreaDto">The updated data for the Area.</param>
    /// <returns>The updated Area.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<AreaDto>> Update(string id, UpdateAreaDto updateAreaDto)
    {
        var area = await _areaService.UpdateAsync(id, updateAreaDto);
        return Ok(area);
    }

    /// <summary>
    /// Delete an Area by its ID.
    /// </summary>
    /// <param name="id">The ID of the Area to delete.</param>
    /// <returns>A 204 No Content response.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _areaService.DeleteAsync(id);
        return NoContent();
    }
}