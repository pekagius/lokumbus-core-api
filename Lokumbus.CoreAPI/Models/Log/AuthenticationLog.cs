using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models.Log
{
    public class AuthenticationLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AuthId { get; set; }
        public string? EventType { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Outcome { get; set; }
        public string? Reason { get; set; }
    }
}