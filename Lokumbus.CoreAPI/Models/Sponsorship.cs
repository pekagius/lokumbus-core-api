using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Sponsorship
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
    }
}