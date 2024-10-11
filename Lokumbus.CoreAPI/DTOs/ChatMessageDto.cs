// ChatMessageDto.cs
namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a ChatMessage.
    /// </summary>
    public class ChatMessageDto : MessageDto
    {
        /// <summary>
        /// The unique identifier of the Chat associated with the message.
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        /// The timestamp when the ChatMessage was received.
        /// </summary>
        public DateTime? ReceivedAt { get; set; }
    }
}