using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für die Verwaltung von AlertMessage-Ressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlertMessagesController : ControllerBase
    {
        private readonly IAlertMessageService _alertMessageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMessagesController"/> class.
        /// </summary>
        /// <param name="alertMessageService">Der AlertMessage-Service.</param>
        public AlertMessagesController(IAlertMessageService alertMessageService)
        {
            _alertMessageService = alertMessageService;
        }

        /// <summary>
        /// Ruft alle AlertMessages ab.
        /// </summary>
        /// <returns>Eine Sammlung von AlertMessageDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertMessageDto>>> GetAll()
        {
            var alertMessages = await _alertMessageService.GetAllAsync();
            return Ok(alertMessages);
        }

        /// <summary>
        /// Ruft eine spezifische AlertMessage anhand der ID ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der AlertMessage.</param>
        /// <returns>Die angeforderte AlertMessageDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertMessageDto>> GetById(string id)
        {
            try
            {
                var alertMessage = await _alertMessageService.GetByIdAsync(id);
                return Ok(alertMessage);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Erstellt eine neue AlertMessage.
        /// </summary>
        /// <param name="createDto">Das DTO mit den Erstellungsdaten.</param>
        /// <returns>Die erstellte AlertMessageDto.</returns>
        [HttpPost]
        public async Task<ActionResult<AlertMessageDto>> Create([FromBody] CreateAlertMessageDto createDto)
        {
            var createdAlertMessage = await _alertMessageService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAlertMessage.Id }, createdAlertMessage);
        }

        /// <summary>
        /// Aktualisiert eine vorhandene AlertMessage.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu aktualisierenden AlertMessage.</param>
        /// <param name="updateDto">Das DTO mit den Aktualisierungsdaten.</param>
        /// <returns>Ein IActionResult, das das Ergebnis angibt.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateAlertMessageDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest(new { Message = "ID stimmt nicht überein." });
            }

            try
            {
                await _alertMessageService.UpdateAsync(updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Löscht eine AlertMessage anhand der ID.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu löschenden AlertMessage.</param>
        /// <returns>Ein IActionResult, das das Ergebnis angibt.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _alertMessageService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}