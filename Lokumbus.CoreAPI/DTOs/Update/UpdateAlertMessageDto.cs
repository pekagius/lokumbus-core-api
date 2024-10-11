using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object for updating an existing AlertMessage.
    /// </summary>
    public class UpdateAlertMessageDto
    {
        /// <summary>
        /// The unique identifier of the AlertMessage to update.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The updated content of the AlertMessage.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// The updated status of the alert.
        /// </summary>
        public MessageStatus? Status { get; set; }

        /// <summary>
        /// The updated subject of the AlertMessage.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// List of Attachment IDs to update.
        /// </summary>
        public List<string>? AttachmentIds { get; set; }
    }
}