namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Ticket.
    /// </summary>
    public class CreateTicketDto
    {
        /// <summary>
        /// The name of the Ticket.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// A brief description of the Ticket.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The price of the Ticket.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The quantity available for the Ticket.
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// The start date and time when the Ticket becomes available.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date and time when the Ticket is no longer available.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Additional metadata associated with the Ticket.
        /// </summary>
        public string? Metadata { get; set; }
    }
}