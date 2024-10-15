using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Sponsorship resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorshipsController : ControllerBase
    {
        private readonly ISponsorshipService _sponsorshipService;

        /// <summary>
        /// Initializes a new instance of the SponsorshipsController class.
        /// </summary>
        /// <param name="sponsorshipService">The Sponsorship service instance.</param>
        public SponsorshipsController(ISponsorshipService sponsorshipService)
        {
            _sponsorshipService = sponsorshipService;
        }

        /// <summary>
        /// Retrieves all Sponsorships.
        /// </summary>
        /// <returns>A collection of SponsorshipDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SponsorshipDto>>> GetAll()
        {
            var sponsorships = await _sponsorshipService.GetAllAsync();
            return Ok(sponsorships);
        }

        /// <summary>
        /// Retrieves a specific Sponsorship by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship.</param>
        /// <returns>The requested SponsorshipDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorshipDto>> GetById(string id)
        {
            try
            {
                var sponsorship = await _sponsorshipService.GetByIdAsync(id);
                return Ok(sponsorship);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the sponsorship does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Sponsorship.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created SponsorshipDto.</returns>
        [HttpPost]
        public async Task<ActionResult<SponsorshipDto>> Create([FromBody] CreateSponsorshipDto createDto)
        {
            var createdSponsorship = await _sponsorshipService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdSponsorship.Id }, createdSponsorship);
        }

        /// <summary>
        /// Updates an existing Sponsorship.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSponsorshipDto updateDto)
        {
            try
            {
                await _sponsorshipService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the sponsorship does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a Sponsorship by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsorship to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _sponsorshipService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the sponsorship does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}