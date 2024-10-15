using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Ticket resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        /// <summary>
        /// Initializes a new instance of the TicketsController class.
        /// </summary>
        /// <param name="ticketService">The Ticket service instance.</param>
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Retrieves all Tickets.
        /// </summary>
        /// <returns>A collection of TicketDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAll()
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// Retrieves a specific Ticket by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket.</param>
        /// <returns>The requested TicketDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(string id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id);
                return Ok(ticket);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the ticket does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Ticket.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created TicketDto.</returns>
        [HttpPost]
        public async Task<ActionResult<TicketDto>> Create([FromBody] CreateTicketDto createDto)
        {
            var createdTicket = await _ticketService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdTicket.Id }, createdTicket);
        }

        /// <summary>
        /// Updates an existing Ticket.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTicketDto updateDto)
        {
            try
            {
                await _ticketService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the ticket does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a Ticket by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Ticket to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _ticketService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the ticket does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}