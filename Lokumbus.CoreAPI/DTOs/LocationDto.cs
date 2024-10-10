using Lokumbus.CoreAPI.Models.ValueObjects;
using JetBrains.Annotations;

namespace Lokumbus.CoreAPI.DTOs;

[UsedImplicitly]
public class LocationDto
{
    /// <summary>
    /// The unique identifier of the Location.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The unique identifier of the AppUser to which the Location belongs.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// The name of the Location.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A brief description of the Location.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The latitude coordinate of the Location.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// The longitude coordinate of the Location.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// The date and time when the Location was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the Location was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the Location is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// The maximum capacity of the Location.
    /// </summary>
    public int? Capacity { get; set; }

    /// <summary>
    /// List of amenities available at the Location.
    /// </summary>
    public List<string>? Amenities { get; set; }

    /// <summary>
    /// Additional metadata associated with the Location.
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// List of Restriction IDs associated with the Location.
    /// </summary>
    public List<string>? RestrictionIds { get; set; }

    /// <summary>
    /// List of Accessibility Feature IDs associated with the Location.
    /// </summary>
    public List<string>? AccessibilityFeatureIds { get; set; }

    /// <summary>
    /// Dictionary indicating the suitability of the Location for various purposes.
    /// </summary>
    public Dictionary<string, bool>? Suitabilities { get; set; }

    /// <summary>
    /// The allowed age range for the Location.
    /// </summary>
    public AgeRange? AllowedAgeRange { get; set; }

    /// <summary>
    /// The identifier of the Area to which the Location belongs.
    /// </summary>
    public string? AreaId { get; set; }

    /// <summary>
    /// The identifier of the Address associated with the Location.
    /// </summary>
    public string? AddressId { get; set; }

    /// <summary>
    /// List of rooms available at the Location.
    /// </summary>
    public List<string>? Rooms { get; set; }
}