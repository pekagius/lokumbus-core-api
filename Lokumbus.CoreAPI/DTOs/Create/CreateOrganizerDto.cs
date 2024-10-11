namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Organizer.
    /// </summary>
    public class CreateOrganizerDto
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
        public Dictionary<string, object>? Metadata { get; set; }
    }
}