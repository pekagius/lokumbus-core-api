using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    public class CreateActivityDto
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DurationMinutes { get; set; }

        public string? LocationId { get; set; }
        public string? MaxParticipants { get; set; }
        public string? MinParticipants { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsActive { get; set; }
        public string? Type { get; set; }
        public string? CategoryId { get; set; }
        public string[]? Tags { get; set; }
        public string[]? Images { get; set; }
        public string[]? Videos { get; set; }
        public string? Url { get; set; }
        public string? TicketUrl { get; set; }
        public string[]? SponsorNames { get; set; }
        public bool? IsSponsorsVisible { get; set; }
        public string? EstimatedTimeInMinutes { get; set; }
        public string[]? Amenities { get; set; }
        public bool? IsOutdoor { get; set; }
        public bool? IsIndoor { get; set; }
        public string[]? Recommendations { get; set; }
        public string[]? Warnings { get; set; }
        public List<MetaEntry>? Metadata { get; set; }
    }
}