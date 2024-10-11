using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.Models.SubClasses
{
    public class AlertMessage : Message
    {
        public string? AlertId { get; set; }
        public Alert? Alert { get; set; }
        
        public override void Send()
    {
        foreach (var channel in Channels)
        {
        
            switch (channel)
            {
                case MessageChannel.Direct:
                    
                    break;
                case MessageChannel.Email:
                  
                    break;
                case MessageChannel.Chat:
                   
                    break;
                case MessageChannel.Kafka:
                    
                    break;
            }
        }
    }
        public override void Retry() { /* Implementierung */ }
        public override void MarkAsDelivered() { /* Implementierung */ }
        public override void MarkAsRead() { /* Implementierung */ }
    }
}