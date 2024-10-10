using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models.ValueObjects
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// The street of the Address.
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// The city of the Address.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// The state or province of the Address.
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// The country of the Address.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// The zip code or postal code of the Address.
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// The latitude coordinate of the Address.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// The longitude coordinate of the Address.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// The date and time when the Address was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the Address was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicates whether the Address is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// The apartment or unit number of the Address.
        /// </summary>
        public string? Apartment { get; set; }

        /// <summary>
        /// The floor or level of the Address.
        /// </summary>
        public string? Floor { get; set; }

        /// <summary>
        /// A brief description of the Address.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Additional metadata associated with the Address.
        /// </summary>
        public string? Metadata { get; set; }
    }
}