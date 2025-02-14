using JetBrains.Annotations;
using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    [UsedImplicitly]
    public class Activity(string id)
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = id;

        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public int? DurationMinutes { get; set; }

        public Location? Location { get; set; }
        public string? MaxParticipants { get; set; }
        public string? MinParticipants { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsActive { get; set; }
        public string? Type { get; set; }
        public Category? Category { get; set; }
        public string[]? Tags { get; set; }
        public string[]? Images { get; set; }
        public string[]? Videos { get; set; }
        public string? Url { get; set; }
        public string? TicketUrl { get; set; }
        public string[]? SponsorNames { get; set; }
        public bool? IsSponsorsVisible { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<string>? Attendees { get; set; }
        public string? AttendeeCount { get; set; }
        public List<Organizer>? Organizers { get; set; }
        public string[]? Equipment { get; set; }
        public string[]? Requirements { get; set; }
        public string? AgeRestriction { get; set; }
        public string[]? SafetyInstructions { get; set; }
        public string[]? Rules { get; set; }
        public string? Difficulty { get; set; }
        public double? Distance { get; set; }
        public string? DistanceUnit { get; set; }
        public string? EstimatedTimeInMinutes { get; set; }
        public string[]? Amenities { get; set; }
        public bool? IsOutdoor { get; set; }
        public bool? IsIndoor { get; set; }
        public string[]? Recommendations { get; set; }
        public string[]? Warnings { get; set; }
        public List<MetaEntry>? Metadata { get; set; }
    }
}