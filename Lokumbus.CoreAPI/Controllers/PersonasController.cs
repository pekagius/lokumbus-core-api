using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

/// <summary>
/// Controller for managing Persona resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonasController : ControllerBase
{
    private readonly IPersonaService _personaService;

    /// <summary>
    /// Initializes a new instance of the PersonasController class.
    /// </summary>
    /// <param name="personaService">The Persona service instance.</param>
    public PersonasController(IPersonaService personaService)
    {
        _personaService = personaService;
    }

    /// <summary>
    /// Retrieves all Personas.
    /// </summary>
    /// <returns>A collection of PersonaDto.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetAll()
    {
        var personas = await _personaService.GetAllAsync();
        return Ok(personas);
    }

    /// <summary>
    /// Retrieves all Personas associated with a specific AppUser.
    /// </summary>
    /// <param name="userId">The unique identifier of the AppUser.</param>
    /// <returns>A collection of PersonaDto associated with the AppUser.</returns>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> GetByUserId(string userId)
    {
        var personas = await _personaService.GetByUserIdAsync(userId);
        return Ok(personas);
    }

    /// <summary>
    /// Retrieves a specific Persona by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona.</param>
    /// <returns>The requested PersonaDto.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaDto>> GetById(string id)
    {
        try
        {
            var persona = await _personaService.GetByIdAsync(id);
            return Ok(persona);
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the persona does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new Persona.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created PersonaDto.</returns>
    [HttpPost]
    public async Task<ActionResult<PersonaDto>> Create([FromBody] CreatePersonaDto createDto)
    {
        var createdPersona = await _personaService.CreateAsync(createDto);
        // Return 201 Created with location header
        return CreatedAtAction(nameof(GetById), new { id = createdPersona.Id }, createdPersona);
    }

    /// <summary>
    /// Updates an existing Persona.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdatePersonaDto updateDto)
    {
        try
        {
            await _personaService.UpdateAsync(id, updateDto);
            // Return 204 No Content on successful update
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the persona does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a Persona by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Persona to delete.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _personaService.DeleteAsync(id);
            // Return 204 No Content on successful deletion
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the persona does not exist
            return NotFound(new { Message = ex.Message });
        }
    }
}