using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller zur Verwaltung von Kalenderressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
    
        /// <summary>
        /// Initialisiert eine neue Instanz der CalendarsController-Klasse.
        /// </summary>
        /// <param name="calendarService">Die Kalender-Service-Instanz.</param>
        public CalendarsController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }
    
        /// <summary>
        /// Ruft alle Kalender ab.
        /// </summary>
        /// <returns>Eine Sammlung von CalendarDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarDto>>> GetAll()
        {
            var calendars = await _calendarService.GetAllAsync();
            return Ok(calendars);
        }
    
        /// <summary>
        /// Ruft einen spezifischen Kalender anhand der ID ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des Kalenders.</param>
        /// <returns>Das angeforderte CalendarDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarDto>> GetById(string id)
        {
            try
            {
                var calendar = await _calendarService.GetByIdAsync(id);
                return Ok(calendar);
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn der Kalender nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }
    
        /// <summary>
        /// Erzeugt einen neuen Kalender.
        /// </summary>
        /// <param name="createDto">Das DTO, das die Erstellungsdaten enthält.</param>
        /// <returns>Das erstellte CalendarDto.</returns>
        [HttpPost]
        public async Task<ActionResult<CalendarDto>> Create([FromBody] CreateCalendarDto createDto)
        {
            var createdCalendar = await _calendarService.CreateAsync(createDto);
            // Rückgabe von 201 Created mit Location-Header
            return CreatedAtAction(nameof(GetById), new { id = createdCalendar.Id }, createdCalendar);
        }
    
        /// <summary>
        /// Aktualisiert einen bestehenden Kalender.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des zu aktualisierenden Kalenders.</param>
        /// <param name="updateDto">Das DTO, das die Aktualisierungsdaten enthält.</param>
        /// <returns>Ein IActionResult, das den Erfolg der Operation anzeigt.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCalendarDto updateDto)
        {
            try
            {
                await _calendarService.UpdateAsync(id, updateDto);
                // Rückgabe von 204 No Content bei erfolgreicher Aktualisierung
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn der Kalender nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }
    
        /// <summary>
        /// Löscht einen Kalender anhand der ID.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung des zu löschenden Kalenders.</param>
        /// <returns>Ein IActionResult, das den Erfolg der Operation anzeigt.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _calendarService.DeleteAsync(id);
                // Rückgabe von 204 No Content bei erfolgreichem Löschen
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe von 404 Not Found, wenn der Kalender nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}