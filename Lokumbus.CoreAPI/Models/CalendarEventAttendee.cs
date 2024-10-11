using Lokumbus.CoreAPI.Models.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class CalendarEventAttendee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public AttendeeStatus Status { get; set; }

        // Geändert von int zu string, um mit der Event-Id übereinzustimmen
        public string CalendarEventId { get; set; }
        
        // Optionale Navigationseigenschaft – kann bei Bedarf verwendet werden
        [BsonIgnore]
        public Event? CalendarEvent { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}