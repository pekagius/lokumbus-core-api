using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für die Verwaltung von Activity-Ressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        /// <summary>
        /// Initialisiert eine neue Instanz der ActivitiesController-Klasse.
        /// </summary>
        /// <param name="activityService">Die Activity-Service-Instanz.</param>
        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        /// <summary>
        /// Ruft alle Activities ab.
        /// </summary>
        /// <returns>Eine Liste von ActivityDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var activities = await _activityService.GetAllAsync();
            return Ok(activities);
        }

        /// <summary>
        /// Ruft eine spezifische Activity anhand ihrer ID ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der Activity.</param>
        /// <returns>Das angeforderte ActivityDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetById(string id)
        {
            try
            {
                var activity = await _activityService.GetByIdAsync(id);
                return Ok(activity);
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn die Activity nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Erstellt eine neue Activity.
        /// </summary>
        /// <param name="createDto">Das DTO, das die Erstellungsdaten enthält.</param>
        /// <returns>Das erstellte ActivityDto.</returns>
        [HttpPost]
        public async Task<ActionResult<ActivityDto>> Create([FromBody] CreateActivityDto createDto)
        {
            var createdActivity = await _activityService.CreateAsync(createDto);
            // Rückgabe von 201 Created mit Location-Header
            return CreatedAtAction(nameof(GetById), new { id = createdActivity.Id }, createdActivity);
        }

        /// <summary>
        /// Aktualisiert eine bestehende Activity.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu aktualisierenden Activity.</param>
        /// <param name="updateDto">Das DTO, das die Aktualisierungsdaten enthält.</param>
        /// <returns>Ein IActionResult, das das Ergebnis der Operation angibt.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityDto>> Update(string id, [FromBody] UpdateActivityDto updateDto)
        {
            try
            {
                var updatedActivity = await _activityService.UpdateAsync(id, updateDto);
                return Ok(updatedActivity);
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn die Activity nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Löscht eine Activity anhand ihrer ID.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu löschenden Activity.</param>
        /// <returns>Ein 204 No Content Response, wenn die Löschung erfolgreich war.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _activityService.DeleteAsync(id);
                // Rückgabe von 204 No Content bei erfolgreicher Löschung
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn die Activity nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}