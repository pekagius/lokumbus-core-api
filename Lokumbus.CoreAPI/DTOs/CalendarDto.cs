using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Calendar.
    /// </summary>
    public class CalendarDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string OwnerId { get; set; }
        public OwnerType OwnerType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsPublic { get; set; }
        public string? TimeZone { get; set; }
        public IEnumerable<EventDto>? Events { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}