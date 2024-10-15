namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing Ticket.
    /// </summary>
    public class UpdateTicketDto
    {
        /// <summary>
        /// The updated name of the Ticket.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The updated description of the Ticket.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The updated price of the Ticket.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// The updated quantity available for the Ticket.
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// The updated start date and time when the Ticket becomes available.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The updated end date and time when the Ticket is no longer available.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Indicates whether the Ticket is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// The date and time when the Ticket was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Additional metadata associated with the Ticket.
        /// </summary>
        public string? Metadata { get; set; }
    }
}