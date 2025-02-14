using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object für die Aktualisierung eines bestehenden Kalenders.
    /// </summary>
    public class UpdateCalendarDto
    {
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
        public bool? IsPublic { get; set; }
        
        /// <summary>
        /// Die Zeitzone des Kalenders.
        /// </summary>
        public string? TimeZone { get; set; }
        
        /// <summary>
        /// Zusätzliche Metadaten für den Kalender.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}