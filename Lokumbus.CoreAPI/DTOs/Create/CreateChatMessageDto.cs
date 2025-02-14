// CreateChatMessageDto.cs

using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new ChatMessage.
    /// </summary>
    public class CreateChatMessageDto
    {
        /// <summary>
        /// The content of the ChatMessage.
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
        /// The unique identifier of the Chat associated with the message.
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// The subject of the ChatMessage.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// The collection of channels through which the Message will be sent.
        /// </summary>
        public ICollection<MessageChannel>? Channels { get; set; } = new List<MessageChannel>();

        /// <summary>
        /// Attachments associated with the ChatMessage.
        /// </summary>
        public ICollection<Attachment>? Attachments { get; set; } = new List<Attachment>();

        /// <summary>
        /// Additional metadata associated with the ChatMessage.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}