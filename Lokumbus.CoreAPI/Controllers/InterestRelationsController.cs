using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für die Verwaltung von InterestRelation Ressourcen.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InterestRelationsController : ControllerBase
    {
        private readonly IInterestRelationService _interestRelationService;

        /// <summary>
        /// Initialisiert eine neue Instanz der InterestRelationsController Klasse.
        /// </summary>
        /// <param name="interestRelationService">Der InterestRelation Service.</param>
        public InterestRelationsController(IInterestRelationService interestRelationService)
        {
            _interestRelationService = interestRelationService;
        }

        /// <summary>
        /// Ruft alle InterestRelations ab.
        /// </summary>
        /// <returns>Eine Liste von InterestRelationDtos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestRelationDto>>> GetAll()
        {
            var interestRelations = await _interestRelationService.GetAllAsync();
            return Ok(interestRelations);
        }

        /// <summary>
        /// Ruft eine spezifische InterestRelation anhand der ID ab.
        /// </summary>
        /// <param name="id">Die ID der InterestRelation.</param>
        /// <returns>Die angeforderte InterestRelationDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<InterestRelationDto>> GetById(string id)
        {
            var interestRelation = await _interestRelationService.GetByIdAsync(id);
            if (interestRelation == null)
            {
                return NotFound(new { Message = $"InterestRelation mit ID {id} wurde nicht gefunden." });
            }
            return Ok(interestRelation);
        }

        /// <summary>
        /// Erstellt eine neue InterestRelation.
        /// </summary>
        /// <param name="createDto">Das DTO mit den Erstellungsdaten.</param>
        /// <returns>Die erstellte InterestRelationDto.</returns>
        [HttpPost]
        public async Task<ActionResult<InterestRelationDto>> Create([FromBody] CreateInterestRelationDto createDto)
        {
            var createdInterestRelation = await _interestRelationService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdInterestRelation.Id }, createdInterestRelation);
        }

        /// <summary>
        /// Aktualisiert eine bestehende InterestRelation.
        /// </summary>
        /// <param name="id">Die ID der zu aktualisierenden InterestRelation.</param>
        /// <param name="updateDto">Das DTO mit den Aktualisierungsdaten.</param>
        /// <returns>Eine NoContent-Antwort, wenn die Aktualisierung erfolgreich war.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateInterestRelationDto updateDto)
        {
            try
            {
                await _interestRelationService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Löscht eine InterestRelation anhand der ID.
        /// </summary>
        /// <param name="id">Die ID der zu löschenden InterestRelation.</param>
        /// <returns>Eine NoContent-Antwort, wenn die Löschung erfolgreich war.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _interestRelationService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}