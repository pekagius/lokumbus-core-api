using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

/// <summary>
/// Controller for managing Reminder resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IReminderService _reminderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemindersController"/> class.
    /// </summary>
    /// <param name="reminderService">The Reminder service instance.</param>
    public RemindersController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    /// <summary>
    /// Retrieves all Reminders.
    /// </summary>
    /// <returns>A collection of ReminderDto.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReminderDto>>> GetAll()
    {
        var reminders = await _reminderService.GetAllAsync();
        return Ok(reminders);
    }

    /// <summary>
    /// Retrieves a specific Reminder by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder.</param>
    /// <returns>The requested ReminderDto.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReminderDto>> GetById(string id)
    {
        var reminder = await _reminderService.GetByIdAsync(id);
        if (reminder == null)
        {
            return NotFound(new { Message = $"Reminder with ID {id} was not found." });
        }
        return Ok(reminder);
    }

    /// <summary>
    /// Creates a new Reminder.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created ReminderDto.</returns>
    [HttpPost]
    public async Task<ActionResult<ReminderDto>> Create([FromBody] CreateReminderDto createDto)
    {
        var createdReminder = await _reminderService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdReminder.Id }, createdReminder);
    }

    /// <summary>
    /// Updates an existing Reminder.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateReminderDto updateDto)
    {
        try
        {
            await _reminderService.UpdateAsync(id, updateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a Reminder by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Reminder to delete.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _reminderService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}