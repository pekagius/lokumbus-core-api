using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Friendship.
    /// </summary>
    public class FriendshipDto
    {
        /// <summary>
        /// Die eindeutige Kennung der Freundschaft.
        /// </summary>
        public string Id { get; set; }
    
        /// <summary>
        /// Die eindeutige Kennung der Persona des Benutzers.
        /// </summary>
        public string PersonaId { get; set; }
    
        /// <summary>
        /// Die zugehörige Persona des Benutzers.
        /// </summary>
        public PersonaDto? Persona { get; set; }
    
        /// <summary>
        /// Die eindeutige Kennung der Persona des Freundes.
        /// </summary>
        public string FriendPersonaId { get; set; }
    
        /// <summary>
        /// Die zugehörige Persona des Freundes.
        /// </summary>
        public PersonaDto? FriendPersona { get; set; }
    
        /// <summary>
        /// Das Erstellungsdatum der Freundschaft.
        /// </summary>
        public DateTime? CreatedAt { get; set; }
    
        /// <summary>
        /// Das Aktualisierungsdatum der Freundschaft.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    
        /// <summary>
        /// Gibt an, ob die Freundschaft akzeptiert wurde.
        /// </summary>
        public bool? IsAccepted { get; set; }
    
        /// <summary>
        /// Zusätzliche Metadaten zur Freundschaft.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}