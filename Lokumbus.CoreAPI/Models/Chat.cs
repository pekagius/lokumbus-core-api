using Confluent.Kafka;
using JetBrains.Annotations;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Chat
    {
        private readonly IProducer<string, string> _producer;
        private readonly IConsumer<string, string> _consumer;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public ICollection<AppUser> Participants { get; set; } = new List<AppUser>();
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        
        // For Validation and Event Handling
        [UsedImplicitly]
        public Chat() { }
        
        public Chat(IProducer<string, string> producer, IConsumer<string, string> consumer, string id)
        {
            _producer = producer;
            _consumer = consumer;
            Id = id;
        }

        public async Task SendMessage(ChatMessage message)
        {
            try
            {
                var kafkaTopic = $"chat-{Id}";
                var kafkaKey = Guid.NewGuid().ToString();

                var messageKey = new Message<string, string>
                {
                    Key = kafkaKey,
                    Value = message.Content
                };

                await _producer.ProduceAsync(kafkaTopic, messageKey);

                message.Status = MessageStatus.Sent;
                message.SentAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                message.Status = MessageStatus.Failed;
                // Fehler loggen oder behandeln
            }
        }

        public async Task<IEnumerable<ChatMessage>> ReceiveMessages()
        {
            var kafkaTopic = $"chat-{Id}";
            var messages = new List<ChatMessage>();

            try
            {
                _consumer.Subscribe(kafkaTopic);

                while (true)
                {
                    var consumeResult = _consumer.Consume();

                    if (consumeResult != null)
                    {
                        var message = new ChatMessage
                        {
                            Content = consumeResult.Message.Value,
                            Status = MessageStatus.Received,
                            ReceivedAt = DateTime.UtcNow
                        };

                        messages.Add(message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                // Fehler loggen oder behandeln
            }
            finally
            {
                _consumer.Unsubscribe();
            }

            return messages;
        }
    }
}