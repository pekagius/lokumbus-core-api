using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Interest resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;

        /// <summary>
        /// Initializes a new instance of the InterestsController class.
        /// </summary>
        /// <param name="interestService">The Interest service instance.</param>
        public InterestsController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        /// <summary>
        /// Retrieves all Interests.
        /// </summary>
        /// <returns>A collection of InterestDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestDto>>> GetAll()
        {
            var interests = await _interestService.GetAllAsync();
            return Ok(interests);
        }

        /// <summary>
        /// Retrieves a specific Interest by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest.</param>
        /// <returns>The requested InterestDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<InterestDto>> GetById(string id)
        {
            try
            {
                var interest = await _interestService.GetByIdAsync(id);
                return Ok(interest);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the interest does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Interest.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created InterestDto.</returns>
        [HttpPost]
        public async Task<ActionResult<InterestDto>> Create([FromBody] CreateInterestDto createDto)
        {
            var createdInterest = await _interestService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdInterest.Id }, createdInterest);
        }

        /// <summary>
        /// Updates an existing Interest.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateInterestDto updateDto)
        {
            try
            {
                await _interestService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the interest does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an Interest by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Interest to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _interestService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the interest does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}