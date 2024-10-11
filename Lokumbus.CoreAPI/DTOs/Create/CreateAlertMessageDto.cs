namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new AlertMessage.
    /// </summary>
    public class CreateAlertMessageDto
    {
        /// <summary>
        /// The content of the AlertMessage.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The system identifier associated with the AlertMessage.
        /// </summary>
        public string? SystemId { get; set; }

        /// <summary>
        /// The subject of the AlertMessage.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// List of Attachment IDs associated with the AlertMessage.
        /// </summary>
        public List<string>? AttachmentIds { get; set; }
    }
}