using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing CalendarEventAttendee resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarEventAttendeesController : ControllerBase
    {
        private readonly ICalendarEventAttendeeService _service;

        /// <summary>
        /// Initializes a new instance of the CalendarEventAttendeesController class.
        /// </summary>
        /// <param name="service">The CalendarEventAttendee service instance.</param>
        public CalendarEventAttendeesController(ICalendarEventAttendeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all CalendarEventAttendees.
        /// </summary>
        /// <returns>A collection of CalendarEventAttendeeDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEventAttendeeDto>>> GetAll()
        {
            var attendees = await _service.GetAllAsync();
            return Ok(attendees);
        }

        /// <summary>
        /// Retrieves a specific CalendarEventAttendee by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee.</param>
        /// <returns>The requested CalendarEventAttendeeDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEventAttendeeDto>> GetById(string id)
        {
            try
            {
                var attendee = await _service.GetByIdAsync(id);
                return Ok(attendee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new CalendarEventAttendee.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created CalendarEventAttendeeDto.</returns>
        [HttpPost]
        public async Task<ActionResult<CalendarEventAttendeeDto>> Create([FromBody] CreateCalendarEventAttendeeDto createDto)
        {
            var createdAttendee = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAttendee.Id }, createdAttendee);
        }

        /// <summary>
        /// Updates an existing CalendarEventAttendee.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateCalendarEventAttendeeDto updateDto)
        {
            try
            {
                await _service.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a CalendarEventAttendee by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the CalendarEventAttendee to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}