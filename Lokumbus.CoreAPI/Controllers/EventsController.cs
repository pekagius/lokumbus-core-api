using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Event resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// Initializes a new instance of the EventsController class.
        /// </summary>
        /// <param name="eventService">The Event service instance.</param>
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Retrieves all Events.
        /// </summary>
        /// <returns>A collection of EventDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        /// <summary>
        /// Retrieves a specific Event by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Event.</param>
        /// <returns>The requested EventDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(string id)
        {
            try
            {
                var @event = await _eventService.GetByIdAsync(id);
                return Ok(@event);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the event does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created EventDto.</returns>
        [HttpPost]
        public async Task<ActionResult<EventDto>> Create([FromBody] CreateEventDto createDto)
        {
            var createdEvent = await _eventService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }

        /// <summary>
        /// Updates an existing Event.
        /// </summary>
        /// <param name="id">The unique identifier of the Event to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateEventDto updateDto)
        {
            try
            {
                await _eventService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the event does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an Event by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Event to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _eventService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the event does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}