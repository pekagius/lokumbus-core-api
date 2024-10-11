using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Definiert die Vertragsmethoden für die Datenzugriffsschicht von InterestRelation.
    /// </summary>
    public interface IInterestRelationRepository
    {
        /// <summary>
        /// Ruft eine InterestRelation anhand der Kennung ab.
        /// </summary>
        /// <param name="id">Die Kennung der InterestRelation.</param>
        /// <returns>Die gefundene InterestRelation oder null.</returns>
        Task<InterestRelation?> GetByIdAsync(string id);

        /// <summary>
        /// Ruft alle InterestRelations ab.
        /// </summary>
        /// <returns>Eine Auflistung aller InterestRelations.</returns>
        Task<IEnumerable<InterestRelation>> GetAllAsync();

        /// <summary>
        /// Erstellt eine neue InterestRelation.
        /// </summary>
        /// <param name="interestRelation">Die zu erstellende InterestRelation.</param>
        Task CreateAsync(InterestRelation interestRelation);

        /// <summary>
        /// Aktualisiert eine bestehende InterestRelation.
        /// </summary>
        /// <param name="interestRelation">Die aktualisierte InterestRelation.</param>
        Task UpdateAsync(InterestRelation interestRelation);

        /// <summary>
        /// Löscht eine InterestRelation anhand der Kennung.
        /// </summary>
        /// <param name="id">Die Kennung der zu löschenden InterestRelation.</param>
        Task DeleteAsync(string id);
    }
}