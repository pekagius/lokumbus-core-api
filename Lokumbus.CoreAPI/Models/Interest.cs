using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Interest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public ICollection<Persona> Personas { get; set; } = new List<Persona>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }
        public string? Popularity { get; set; }
        public ICollection<InterestRelation> RelatedInterests { get; set; } = new List<InterestRelation>();
        public List<MetaEntry>? Metadata { get; set; }
    }
}