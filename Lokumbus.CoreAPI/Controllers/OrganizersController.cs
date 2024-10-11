using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Organizer resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizersController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        /// <summary>
        /// Initializes a new instance of the OrganizersController class.
        /// </summary>
        /// <param name="organizerService">The Organizer service instance.</param>
        public OrganizersController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        /// <summary>
        /// Retrieves all Organizers.
        /// </summary>
        /// <returns>A collection of OrganizerDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizerDto>>> GetAll()
        {
            var organizers = await _organizerService.GetAllAsync();
            return Ok(organizers);
        }

        /// <summary>
        /// Retrieves a specific Organizer by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer.</param>
        /// <returns>The requested OrganizerDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizerDto>> GetById(string id)
        {
            try
            {
                var organizer = await _organizerService.GetByIdAsync(id);
                return Ok(organizer);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the organizer does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Organizer.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created OrganizerDto.</returns>
        [HttpPost]
        public async Task<ActionResult<OrganizerDto>> Create([FromBody] CreateOrganizerDto createDto)
        {
            var createdOrganizer = await _organizerService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdOrganizer.Id }, createdOrganizer);
        }

        /// <summary>
        /// Updates an existing Organizer.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateOrganizerDto updateDto)
        {
            try
            {
                await _organizerService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the organizer does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an Organizer by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Organizer to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _organizerService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the organizer does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}