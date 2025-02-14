using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing an AlertMessage.
    /// </summary>
    public class AlertMessageDto
    {
        /// <summary>
        /// The unique identifier of the AlertMessage.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The content of the AlertMessage.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The system identifier associated with the AlertMessage.
        /// </summary>
        public string? SystemId { get; set; }

        /// <summary>
        /// The status of the alert.
        /// </summary>
        public MessageStatus? Status { get; set; }

        /// <summary>
        /// The timestamp when the alert was sent.
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// The timestamp when the alert was delivered.
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// The timestamp when the alert was read.
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// The subject of the alert.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Attachments associated with the AlertMessage.
        /// </summary>
        public List<Attachment>? Attachments { get; set; }

        /// <summary>
        /// Additional metadata associated with the AlertMessage.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}