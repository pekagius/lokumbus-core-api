using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Definiert die Vertragsmethoden für die Geschäftslogik von InterestRelation.
    /// </summary>
    public interface IInterestRelationService
    {
        /// <summary>
        /// Ruft eine InterestRelationDto anhand der Kennung ab.
        /// </summary>
        /// <param name="id">Die Kennung der InterestRelation.</param>
        /// <returns>Die gefundene InterestRelationDto oder null.</returns>
        Task<InterestRelationDto?> GetByIdAsync(string id);

        /// <summary>
        /// Ruft alle InterestRelationDtos ab.
        /// </summary>
        /// <returns>Eine Auflistung aller InterestRelationDtos.</returns>
        Task<IEnumerable<InterestRelationDto>> GetAllAsync();

        /// <summary>
        /// Erstellt eine neue InterestRelation.
        /// </summary>
        /// <param name="createDto">Das DTO mit den Erstellungsdaten.</param>
        /// <returns>Die erstellte InterestRelationDto.</returns>
        Task<InterestRelationDto> CreateAsync(CreateInterestRelationDto createDto);

        /// <summary>
        /// Aktualisiert eine bestehende InterestRelation.
        /// </summary>
        /// <param name="id">Die Kennung der zu aktualisierenden InterestRelation.</param>
        /// <param name="updateDto">Das DTO mit den Aktualisierungsdaten.</param>
        Task UpdateAsync(string id, UpdateInterestRelationDto updateDto);

        /// <summary>
        /// Löscht eine InterestRelation anhand der Kennung.
        /// </summary>
        /// <param name="id">Die Kennung der zu löschenden InterestRelation.</param>
        Task DeleteAsync(string id);
    }
}