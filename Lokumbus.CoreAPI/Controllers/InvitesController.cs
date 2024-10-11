using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für die Verwaltung von Invite Ressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvitesController : ControllerBase
    {
        private readonly IInviteService _inviteService;

        /// <summary>
        /// Initialisiert eine neue Instanz der InvitesController Klasse.
        /// </summary>
        /// <param name="inviteService">Der Invite Service.</param>
        public InvitesController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        /// <summary>
        /// Ruft alle Invites ab.
        /// </summary>
        /// <returns>Eine Liste von InviteDtos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InviteDto>>> GetAll()
        {
            var invites = await _inviteService.GetAllAsync();
            return Ok(invites);
        }

        /// <summary>
        /// Ruft eine spezifische Invite anhand der ID ab.
        /// </summary>
        /// <param name="id">Die ID der Invite.</param>
        /// <returns>Die angeforderte InviteDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<InviteDto>> GetById(string id)
        {
            var invite = await _inviteService.GetByIdAsync(id);
            if (invite == null)
            {
                return NotFound(new { Message = $"Invite mit ID {id} wurde nicht gefunden." });
            }
            return Ok(invite);
        }

        /// <summary>
        /// Erstellt eine neue Invite.
        /// </summary>
        /// <param name="createDto">Das DTO mit den Erstellungsdaten.</param>
        /// <returns>Die erstellte InviteDto.</returns>
        [HttpPost]
        public async Task<ActionResult<InviteDto>> Create([FromBody] CreateInviteDto createDto)
        {
            var createdInvite = await _inviteService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdInvite.Id }, createdInvite);
        }

        /// <summary>
        /// Aktualisiert eine bestehende Invite.
        /// </summary>
        /// <param name="id">Die ID der zu aktualisierenden Invite.</param>
        /// <param name="updateDto">Das DTO mit den Aktualisierungsdaten.</param>
        /// <returns>Eine NoContent-Antwort, wenn die Aktualisierung erfolgreich war.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateInviteDto updateDto)
        {
            try
            {
                await _inviteService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Löscht eine Invite anhand der ID.
        /// </summary>
        /// <param name="id">Die ID der zu löschenden Invite.</param>
        /// <returns>Eine NoContent-Antwort, wenn die Löschung erfolgreich war.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _inviteService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}