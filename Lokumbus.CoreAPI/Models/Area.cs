using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Area
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The name of the Area.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A brief description of the Area.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The type of the Area.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// The latitude coordinate of the Area.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// The longitude coordinate of the Area.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// The radius of the Area.
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// The boundaries of the Area.
        /// </summary>
        public List<Coordinate>? Boundaries { get; set; }

        /// <summary>
        /// The identifier of the parent Area.
        /// </summary>
        public string? ParentAreaId { get; set; }

        /// <summary>
        /// The identifiers of the child Areas.
        /// </summary>
        public List<string>? ChildAreaIds { get; set; }

        /// <summary>
        /// The date and time when the Area was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the Area was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicates whether the Area is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// The time zone of the Area.
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// The URL of the image associated with the Area.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// The tags associated with the Area.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Additional metadata associated with the Area.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }

        /// <summary>
        /// The identifiers of the related Locations.
        /// </summary>
        public List<string>? RelatedLocationIds { get; set; }

        /// <summary>
        /// The identifiers of the related Events.
        /// </summary>
        public List<string>? RelatedEventIds { get; set; }
    }
}