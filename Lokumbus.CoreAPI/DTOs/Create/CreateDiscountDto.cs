namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Discount.
    /// </summary>
    public class CreateDiscountDto
    {
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
        /// The start date of the Discount validity.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date of the Discount validity.
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}