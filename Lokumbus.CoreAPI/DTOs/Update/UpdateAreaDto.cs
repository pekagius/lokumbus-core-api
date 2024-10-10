using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.DTOs.Update;

public class UpdateAreaDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Radius { get; set; }
    public List<Coordinate>? Boundaries { get; set; }
    public string? ParentAreaId { get; set; }
    public List<string>? ChildAreaIds { get; set; }
    public string? TimeZone { get; set; }
    public string? ImageUrl { get; set; }
    public List<string>? Tags { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
    public List<string>? RelatedLocationIds { get; set; }
    public List<string>? RelatedEventIds { get; set; }
}