using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Persona
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public List<string>? CalendarIds { get; set; } = [];
        public List<string>? EventIds { get; set; } = [];
        public List<string>? TicketIds { get; set; } = [];
        public List<string>? InviteIds { get; set; } = [];
        public List<string>? FriendshipIds { get; set; } = [];
        public List<string>? ChatIds { get; set; } = [];
        public List<string>? ChatMessageIds { get; set; } = [];
        public List<string>? NotificationIds { get; set; } = [];
        public List<string>? ReviewIds { get; set; } = [];
        public List<string>? InterestIds { get; set; } = [];
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}