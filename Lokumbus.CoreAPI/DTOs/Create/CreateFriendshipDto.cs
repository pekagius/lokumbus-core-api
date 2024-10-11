namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object für die Erstellung einer neuen Friendship.
    /// </summary>
    public class CreateFriendshipDto
    {
        /// <summary>
        /// Die eindeutige Kennung der Persona des Benutzers.
        /// </summary>
        public string PersonaId { get; set; }
    
        /// <summary>
        /// Die eindeutige Kennung der Persona des Freundes.
        /// </summary>
        public string FriendPersonaId { get; set; }
    }
}