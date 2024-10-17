using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class AlertMessageMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateAlertMessageDto, AlertMessage>()
                .Map(dest => dest.Status, src => MessageStatus.Sent)
                .Map(dest => dest.SentAt, src => DateTime.UtcNow)
                .Map(dest => dest.SystemId, src => src.SystemId);

            config.NewConfig<UpdateAlertMessageDto, AlertMessage>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.SentAt)
                .Ignore(dest => dest.DeliveredAt)
                .Ignore(dest => dest.ReadAt)
                .Ignore(dest => dest.SystemId);

            config.NewConfig<AlertMessage, AlertMessageDto>();
        }
    }
}