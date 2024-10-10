using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class AuthorizationLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AuthId { get; set; }
        public string? ResourceType { get; set; }
        public string? ResourceId { get; set; }
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Outcome { get; set; }
        public string? Reason { get; set; }
    }
}