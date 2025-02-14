using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing Interest.
    /// </summary>
    public class UpdateInterestDto
    {
        /// <summary>
        /// The updated name of the Interest.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The updated description of the Interest.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The updated category ID associated with the Interest.
        /// </summary>
        public string? CategoryId { get; set; }

        /// <summary>
        /// The updated collection of Event IDs related to the Interest.
        /// </summary>
        public List<string>? EventIds { get; set; } = new();

        /// <summary>
        /// The updated collection of User IDs associated with the Interest.
        /// </summary>
        public List<string>? UserIds { get; set; } = new();

        /// <summary>
        /// Indicates whether the Interest is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Updated metadata associated with the Interest.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}