namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing an Interest.
    /// </summary>
    public class InterestDto
    {
        /// <summary>
        /// The unique identifier of the Interest.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the Interest.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A brief description of the Interest.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The category ID associated with the Interest.
        /// </summary>
        public string? CategoryId { get; set; }

        /// <summary>
        /// A collection of Event IDs related to the Interest.
        /// </summary>
        public List<string>? EventIds { get; set; }

        /// <summary>
        /// A collection of User IDs associated with the Interest.
        /// </summary>
        public List<string>? UserIds { get; set; }

        /// <summary>
        /// Indicates whether the Interest is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Metadata associated with the Interest.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}