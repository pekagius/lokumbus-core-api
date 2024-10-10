using Lokumbus.CoreAPI.Models.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Calendar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string OwnerId { get; set; }
        public OwnerType OwnerType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsPublic { get; set; }
        public string? TimeZone { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public Dictionary<string, object>? Metadata { get; set; }

        public void AddEvent(Event @event)
        {
            Events.Add(@event);
            @event.Calendars.Add(this);
        }

        public void RemoveEvent(Event @event)
        {
            Events.Remove(@event);
            @event.Calendars.Remove(this);
        }

        public IEnumerable<Event> GetEvents(DateTime startDate, DateTime endDate)
        {
            return Events.Where(e => e.StartDateTime >= startDate && e.EndDateTime <= endDate);
        }
    }
}

