using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Invite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
    }
}