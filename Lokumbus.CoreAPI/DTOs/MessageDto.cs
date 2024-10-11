using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;


namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Base Data Transfer Object representing a Message.
    /// </summary>
    public abstract class MessageDto
    {
        /// <summary>
        /// The unique identifier of the Message.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The content of the Message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The sender's AppUser ID.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// The recipient's AppUser ID.
        /// </summary>
        public string RecipientId { get; set; }

        /// <summary>
        /// The system identifier associated with the Message.
        /// </summary>
        public string? SystemId { get; set; }

        /// <summary>
        /// The collection of channels through which the Message was sent.
        /// </summary>
        public ICollection<MessageChannel>? Channels { get; set; }

        /// <summary>
        /// The timestamp when the Message was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The timestamp when the Message was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The status of the Message.
        /// </summary>
        public MessageStatus? Status { get; set; }

        /// <summary>
        /// The timestamp when the Message was sent.
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// The timestamp when the Message was delivered.
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// The timestamp when the Message was read.
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// The subject of the Message.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Attachments associated with the Message.
        /// </summary>
        public ICollection<Attachment>? Attachments { get; set; }

        /// <summary>
        /// Additional metadata associated with the Message.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}