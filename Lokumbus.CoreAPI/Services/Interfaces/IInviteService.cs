using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Definiert die Vertragsmethoden für die Geschäftslogik von Invite.
    /// </summary>
    public interface IInviteService
    {
        /// <summary>
        /// Ruft eine InviteDto anhand der Kennung ab.
        /// </summary>
        /// <param name="id">Die Kennung der Invite.</param>
        /// <returns>Die gefundene InviteDto oder null.</returns>
        Task<InviteDto?> GetByIdAsync(string id);

        /// <summary>
        /// Ruft alle InviteDtos ab.
        /// </summary>
        /// <returns>Eine Auflistung aller InviteDtos.</returns>
        Task<IEnumerable<InviteDto>> GetAllAsync();

        /// <summary>
        /// Erstellt eine neue Invite.
        /// </summary>
        /// <param name="createDto">Das DTO mit den Erstellungsdaten.</param>
        /// <returns>Die erstellte InviteDto.</returns>
        Task<InviteDto> CreateAsync(CreateInviteDto createDto);

        /// <summary>
        /// Aktualisiert eine bestehende Invite.
        /// </summary>
        /// <param name="id">Die Kennung der zu aktualisierenden Invite.</param>
        /// <param name="updateDto">Das DTO mit den Aktualisierungsdaten.</param>
        Task UpdateAsync(string id, UpdateInviteDto updateDto);

        /// <summary>
        /// Löscht eine Invite anhand der Kennung.
        /// </summary>
        /// <param name="id">Die Kennung der zu löschenden Invite.</param>
        Task DeleteAsync(string id);
    }
}