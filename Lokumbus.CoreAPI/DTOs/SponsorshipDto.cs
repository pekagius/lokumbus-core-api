namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Sponsorship.
    /// </summary>
    public class SponsorshipDto
    {
        /// <summary>
        /// The unique identifier of the Sponsorship.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the Sponsorship.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A brief description of the Sponsorship.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The monetary amount of the Sponsorship.
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The date and time when the Sponsorship was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the Sponsorship was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicates whether the Sponsorship is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Additional metadata associated with the Sponsorship.
        /// </summary>
        public string? Metadata { get; set; }
    }
}