using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für die Verwaltung von Friendship-Ressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipsController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        /// <summary>
        /// Initialisiert eine neue Instanz der FriendshipsController-Klasse.
        /// </summary>
        /// <param name="friendshipService">Die Friendship-Service-Instanz.</param>
        public FriendshipsController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        /// <summary>
        /// Ruft alle Friendships ab.
        /// </summary>
        /// <returns>Eine Sammlung von FriendshipDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetAll()
        {
            var friendships = await _friendshipService.GetAllAsync();
            return Ok(friendships);
        }

        /// <summary>
        /// Ruft alle Friendships eines bestimmten Benutzers ab.
        /// </summary>
        /// <param name="personaId">Die eindeutige Kennung der Persona des Benutzers.</param>
        /// <returns>Eine Sammlung von FriendshipDto des Benutzers.</returns>
        [HttpGet("persona/{personaId}")]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetByPersonaId(string personaId)
        {
            var friendships = await _friendshipService.GetByPersonaIdAsync(personaId);
            return Ok(friendships);
        }

        /// <summary>
        /// Ruft eine spezifische Friendship anhand ihrer ID ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der Friendship.</param>
        /// <returns>Das angeforderte FriendshipDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendshipDto>> GetById(string id)
        {
            try
            {
                var friendship = await _friendshipService.GetByIdAsync(id);
                return Ok(friendship);
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found zurückgeben, wenn die Friendship nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Erstellt eine neue Friendship.
        /// </summary>
        /// <param name="createDto">Das DTO, das die Erstellungsdaten enthält.</param>
        /// <returns>Das erstellte FriendshipDto.</returns>
        [HttpPost]
        public async Task<ActionResult<FriendshipDto>> Create([FromBody] CreateFriendshipDto createDto)
        {
            var createdFriendship = await _friendshipService.CreateAsync(createDto);
            // 201 Created mit Location-Header zurückgeben
            return CreatedAtAction(nameof(GetById), new { id = createdFriendship.Id }, createdFriendship);
        }

        /// <summary>
        /// Aktualisiert eine bestehende Friendship.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu aktualisierenden Friendship.</param>
        /// <param name="updateDto">Das DTO, das die Aktualisierungsdaten enthält.</param>
        /// <returns>Ein IActionResult, das das Ergebnis angibt.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateFriendshipDto updateDto)
        {
            try
            {
                await _friendshipService.UpdateAsync(id, updateDto);
                // 204 No Content bei erfolgreicher Aktualisierung zurückgeben
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found zurückgeben, wenn die Friendship nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Löscht eine Friendship anhand ihrer ID.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu löschenden Friendship.</param>
        /// <returns>Ein IActionResult, das das Ergebnis angibt.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _friendshipService.DeleteAsync(id);
                // 204 No Content bei erfolgreichem Löschen zurückgeben
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found zurückgeben, wenn die Friendship nicht existiert
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}