using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Definiert die Vertragsmethoden für die Datenzugriffsschicht von Invite.
    /// </summary>
    public interface IInviteRepository
    {
        /// <summary>
        /// Ruft eine Invite anhand der Kennung ab.
        /// </summary>
        /// <param name="id">Die Kennung der Invite.</param>
        /// <returns>Die gefundene Invite oder null.</returns>
        Task<Invite?> GetByIdAsync(string id);

        /// <summary>
        /// Ruft alle Invites ab.
        /// </summary>
        /// <returns>Eine Auflistung aller Invites.</returns>
        Task<IEnumerable<Invite>> GetAllAsync();

        /// <summary>
        /// Erstellt eine neue Invite.
        /// </summary>
        /// <param name="invite">Die zu erstellende Invite.</param>
        Task CreateAsync(Invite invite);

        /// <summary>
        /// Aktualisiert eine bestehende Invite.
        /// </summary>
        /// <param name="invite">Die Invite mit aktualisierten Informationen.</param>
        Task UpdateAsync(Invite invite);

        /// <summary>
        /// Löscht eine Invite anhand der Kennung.
        /// </summary>
        /// <param name="id">Die Kennung der zu löschenden Invite.</param>
        Task DeleteAsync(string id);
    }
}