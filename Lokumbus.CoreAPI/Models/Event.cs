using System.Globalization;
using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool? IsAllDay { get; set; }
        public string? Recurrence { get; set; }
        public string? ReminderId { get; set; }
        public Reminder? Reminder { get; set; }
        public string? LocationId { get; set; }
        public Location? Location { get; set; }
        public string? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }
        public ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
        public ICollection<AppUser> Attendees { get; set; } = new List<AppUser>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Interest> Interests { get; set; } = new List<Interest>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsCancelled { get; set; }
        public string? Status { get; set; }
        public string? Capacity { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Slug { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Sponsorship> Sponsorships { get; set; } = new List<Sponsorship>();
        public ICollection<Grant> Grants { get; set; } = new List<Grant>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<Invite> Invites { get; set; } = new List<Invite>();
        public Dictionary<string, object>? Metadata { get; set; }
    }
}