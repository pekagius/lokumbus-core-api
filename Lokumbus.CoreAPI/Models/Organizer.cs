using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Organizer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Sponsorship> Sponsorships { get; set; } = new List<Sponsorship>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<Invite> Invites { get; set; } = new List<Invite>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public string? Status { get; set; }
        public List<MetaEntry>? Metadata { get; set; }
    }
}