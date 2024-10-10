using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.Models.SubClasses
{
    public class NotificationMessage : Message
    {
        public string? NotificationId { get; set; }
        public Notification? Notification { get; set; }
        // Zusätzliche Eigenschaften und Methoden für NotificationMessage
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
        public override void Retry() { /* Implementierung */ }
        public override void MarkAsDelivered() { /* Implementierung */ }
        public override void MarkAsRead() { /* Implementierung */ }
    }
}