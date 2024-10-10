using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public abstract class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Content { get; set; }
        public string? SenderId { get; set; }
        public AppUser? Sender { get; set; }
        public string? RecipientId { get; set; }
        public AppUser? Recipient { get; set; }
        public string? SystemId { get; set; }
        public MessageSystem? System { get; set; }
        public ICollection<MessageChannel> Channels { get; set; } = new List<MessageChannel>();
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public MessageStatus? Status { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? Subject { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public Dictionary<string, object>? Metadata { get; set; }

        public abstract void Send();
        public abstract void Retry();
        public abstract void MarkAsDelivered();
        public abstract void MarkAsRead();
    }
}