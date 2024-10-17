using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class InterestMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateInterestDto, Interest>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => src.IsActive ?? true);

            config.NewConfig<UpdateInterestDto, Interest>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.Metadata);

            config.NewConfig<Interest, InterestDto>();
        }
    }
}