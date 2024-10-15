namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing Sponsorship.
    /// </summary>
    public class UpdateSponsorshipDto
    {
        /// <summary>
        /// The updated name of the Sponsorship.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The updated description of the Sponsorship.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The updated monetary amount of the Sponsorship.
        /// </summary>
        public decimal? Amount { get; set; }

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