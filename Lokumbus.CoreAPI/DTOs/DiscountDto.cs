namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Discount.
    /// </summary>
    public class DiscountDto
    {
        /// <summary>
        /// The unique identifier of the Discount.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the Discount.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A brief description of the Discount.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The monetary amount of the Discount.
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The code associated with the Discount.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// The date and time when the Discount was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the Discount was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The start date of the Discount validity.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date of the Discount validity.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}