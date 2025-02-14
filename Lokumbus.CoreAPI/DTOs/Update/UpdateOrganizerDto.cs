using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing Organizer.
    /// </summary>
    public class UpdateOrganizerDto
    {
        /// <summary>
        /// The name of the Organizer.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email address of the Organizer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number of the Organizer.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// The website URL of the Organizer.
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// A brief description of the Organizer.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The URL to the Organizer's logo image.
        /// </summary>
        public string? LogoUrl { get; set; }

        /// <summary>
        /// The address ID associated with the Organizer.
        /// </summary>
        public string? AddressId { get; set; }

        /// <summary>
        /// Metadata associated with the Organizer.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }

        /// <summary>
        /// The date and time when the Organizer was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicates whether the Organizer is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// The status of the Organizer.
        /// </summary>
        public string? Status { get; set; }
    }
}