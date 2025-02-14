using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Friendship
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PersonaId { get; set; }
        public Persona? Persona { get; set; }
        public string FriendPersonaId { get; set; }
        public Persona? FriendPersona { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsAccepted { get; set; }
        public List<MetaEntry>? Metadata { get; set; }
    }
}