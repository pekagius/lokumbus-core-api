using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class ChatMessageMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateChatMessageDto, ChatMessage>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.SentAt, src => DateTime.UtcNow)
                .Map(dest => dest.Status, src => MessageStatus.Sent);

            config.NewConfig<UpdateChatMessageDto, ChatMessage>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.SentAt)
                .Ignore(dest => dest.DeliveredAt)
                .Ignore(dest => dest.ReadAt)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            config.NewConfig<ChatMessage, ChatMessageDto>();
        }
    }
}