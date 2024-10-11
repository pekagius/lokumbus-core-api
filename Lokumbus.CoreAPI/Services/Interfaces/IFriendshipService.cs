using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Definiert den Vertrag für Friendship-Service-Operationen.
    /// </summary>
    public interface IFriendshipService
    {
        /// <summary>
        /// Ruft eine Friendship anhand ihrer eindeutigen Kennung ab.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der Friendship.</param>
        /// <returns>Das FriendshipDto, falls gefunden; andernfalls null.</returns>
        Task<FriendshipDto> GetByIdAsync(string id);
    
        /// <summary>
        /// Ruft alle Friendships ab.
        /// </summary>
        /// <returns>Eine Sammlung aller FriendshipDtos.</returns>
        Task<IEnumerable<FriendshipDto>> GetAllAsync();
    
        /// <summary>
        /// Ruft alle Friendships eines bestimmten Benutzers ab.
        /// </summary>
        /// <param name="personaId">Die eindeutige Kennung der Persona des Benutzers.</param>
        /// <returns>Eine Sammlung der FriendshipDtos des Benutzers.</returns>
        Task<IEnumerable<FriendshipDto>> GetByPersonaIdAsync(string personaId);
    
        /// <summary>
        /// Erstellt eine neue Friendship.
        /// </summary>
        /// <param name="createDto">Das DTO, das die Daten für die Erstellung enthält.</param>
        /// <returns>Das erstellte FriendshipDto.</returns>
        Task<FriendshipDto> CreateAsync(CreateFriendshipDto createDto);
    
        /// <summary>
        /// Aktualisiert eine bestehende Friendship.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der Friendship, die aktualisiert werden soll.</param>
        /// <param name="updateDto">Das DTO, das die Aktualisierungsdaten enthält.</param>
        Task UpdateAsync(string id, UpdateFriendshipDto updateDto);
    
        /// <summary>
        /// Löscht eine Friendship anhand ihrer eindeutigen Kennung.
        /// </summary>
        /// <param name="id">Die eindeutige Kennung der zu löschenden Friendship.</param>
        Task DeleteAsync(string id);
    }
}