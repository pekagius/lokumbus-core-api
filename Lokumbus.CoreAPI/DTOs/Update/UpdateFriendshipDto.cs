using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object für die Aktualisierung einer bestehenden Friendship.
    /// </summary>
    public class UpdateFriendshipDto
    {
        /// <summary>
        /// Gibt an, ob die Freundschaft akzeptiert wurde.
        /// </summary>
        public bool? IsAccepted { get; set; }
    
        /// <summary>
        /// Das Aktualisierungsdatum der Freundschaft.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    
        /// <summary>
        /// Zusätzliche Metadaten zur Freundschaft.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}