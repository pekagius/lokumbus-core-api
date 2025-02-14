// UpdateChatMessageDto.cs

using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing ChatMessage.
    /// </summary>
    public class UpdateChatMessageDto
    {
        /// <summary>
        /// The unique identifier of the ChatMessage to update.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The updated content of the ChatMessage.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The updated status of the message.
        /// </summary>
        public MessageStatus? Status { get; set; }

        /// <summary>
        /// The updated subject of the ChatMessage.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Attachments associated with the ChatMessage.
        /// </summary>
        public ICollection<Attachment>? Attachments { get; set; }

        /// <summary>
        /// Additional metadata associated with the ChatMessage.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }

        /// <summary>
        /// The collection of channels through which the Message is sent.
        /// </summary>
        public ICollection<MessageChannel>? Channels { get; set; }
    }
}