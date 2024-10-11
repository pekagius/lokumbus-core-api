using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Definiert den Vertrag für Friendship-Repository-Operationen.
    /// </summary>
    public interface IFriendshipRepository
    {
        /// <summary>
        /// Ruft eine Friendship anhand ihrer eindeutigen Kennung ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der Friendship.</param>
        /// <returns>Die Friendship, falls gefunden; andernfalls null.</returns>
        Task<Friendship?> GetByIdAsync(string id);
    
        /// <summary>
        /// Ruft alle Friendships ab.
        /// </summary>
        /// <returns>Eine Sammlung aller Friendships.</returns>
        Task<IEnumerable<Friendship>> GetAllAsync();
    
        /// <summary>
        /// Ruft alle Friendships eines bestimmten Benutzers ab.
        /// </summary>
        /// <param name="personaId">Die eindeutige Kennung der Persona des Benutzers.</param>
        /// <returns>Eine Sammlung der Friendships des Benutzers.</returns>
        Task<IEnumerable<Friendship>> GetByPersonaIdAsync(string personaId);
    
        /// <summary>
        /// Erstellt eine neue Friendship.
        /// </summary>
        /// <param name="friendship">Die zu erstellende Friendship.</param>
        Task CreateAsync(Friendship friendship);
    
        /// <summary>
        /// Aktualisiert eine bestehende Friendship.
        /// </summary>
        /// <param name="friendship">Die aktualisierte Friendship.</param>
        Task UpdateAsync(Friendship friendship);
    
        /// <summary>
        /// Löscht eine Friendship anhand ihrer eindeutigen Kennung.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu löschenden Friendship.</param>
        Task DeleteAsync(string id);
    }
}