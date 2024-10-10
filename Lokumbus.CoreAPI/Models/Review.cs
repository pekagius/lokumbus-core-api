using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PersonaId { get; set; }
        public Persona? Persona { get; set; }
        public string ReviewedEntityId { get; set; }
        public string? ReviewedEntityType { get; set; }
        public string Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}