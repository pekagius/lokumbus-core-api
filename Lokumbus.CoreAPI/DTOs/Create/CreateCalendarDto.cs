using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Calendar.
    /// </summary>
    public class CreateCalendarDto
    {
        /// <summary>
        /// Der eindeutige Bezeichner des Besitzers (AppUser/Persona/Organizer).
        /// </summary>
        public string OwnerId { get; set; }
        
        /// <summary>
        /// Der Typ des Besitzers (User, Persona, Organizer).
        /// </summary>
        public OwnerType OwnerType { get; set; }
        
        /// <summary>
        /// Der Name des Kalenders.
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// Eine kurze Beschreibung des Kalenders.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gibt an, ob der Kalender öffentlich zugänglich ist.
        /// </summary>
        public bool IsPublic { get; set; } = true;
        
        /// <summary>
        /// Die Zeitzone des Kalenders.
        /// </summary>
        public string? TimeZone { get; set; }
        
        /// <summary>
        /// Zusätzliche Metadaten für den Kalender.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}