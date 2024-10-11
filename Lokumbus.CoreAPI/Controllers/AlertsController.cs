using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Alert resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        /// <summary>
        /// Initializes a new instance of the AlertsController class.
        /// </summary>
        /// <param name="alertService">The Alert service instance.</param>
        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        /// <summary>
        /// Retrieves all Alerts.
        /// </summary>
        /// <returns>A collection of AlertDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertDto>>> GetAll()
        {
            var alerts = await _alertService.GetAllAsync();
            return Ok(alerts);
        }

        /// <summary>
        /// Retrieves a specific Alert by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert.</param>
        /// <returns>The requested AlertDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertDto>> GetById(string id)
        {
            try
            {
                var alert = await _alertService.GetByIdAsync(id);
                return Ok(alert);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the alert does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Alert.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created AlertDto.</returns>
        [HttpPost]
        public async Task<ActionResult<AlertDto>> Create([FromBody] CreateAlertDto createDto)
        {
            var createdAlert = await _alertService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdAlert.Id }, createdAlert);
        }

        /// <summary>
        /// Updates an existing Alert.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAlertDto updateDto)
        {
            try
            {
                await _alertService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the alert does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an Alert by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Alert to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _alertService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the alert does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}