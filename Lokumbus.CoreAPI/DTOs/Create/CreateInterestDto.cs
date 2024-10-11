namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Interest.
    /// </summary>
    public class CreateInterestDto
    {
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
        public List<string>? EventIds { get; set; } = new();

        /// <summary>
        /// A collection of User IDs associated with the Interest.
        /// </summary>
        public List<string>? UserIds { get; set; } = new();

        /// <summary>
        /// Indicates whether the Interest is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Additional metadata associated with the Interest.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}