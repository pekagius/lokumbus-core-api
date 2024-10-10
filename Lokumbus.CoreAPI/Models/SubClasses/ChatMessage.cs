using Lokumbus.CoreAPI.Models.Enumerations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models.SubClasses
{
    public class ChatMessage : Message
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ChatId { get; set; } // Geändert von string? zu string?

        [BsonIgnore]
        public Chat? Chat { get; set; }

        public DateTime ReceivedAt { get; set; }

        // Zusätzliche Eigenschaften und Methoden für ChatMessage
        
        public override void Send()
        {
            foreach (var channel in Channels)
            {
                // Implementierung des Sendens für jeden Kanal
                switch (channel)
                {
                    case MessageChannel.Direct:
                        // Senden über den direkten Kanal
                        break;
                    case MessageChannel.Email:
                        // Senden über den E-Mail-Kanal
                        break;
                    case MessageChannel.Chat:
                        // Senden über den Chat-Kanal
                        break;
                    case MessageChannel.Kafka:
                        // Senden über den Kafka-Kanal
                        break;
                    // Weitere Kanäle hinzufügen...
                }
            }
        }

        public override void Retry()
        {
            // Implementierung
        }

        public override void MarkAsDelivered()
        {
            // Implementierung
        }

        public override void MarkAsRead()
        {
            // Implementierung
        }
    }
}