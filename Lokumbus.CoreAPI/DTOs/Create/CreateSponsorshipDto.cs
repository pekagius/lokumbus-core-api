using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Sponsorship.
    /// </summary>
    public class CreateSponsorshipDto
    {
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
        /// Additional metadata associated with the Sponsorship.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}