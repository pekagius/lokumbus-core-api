using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class DiscountMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateDiscountDto, Discount>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            config.NewConfig<UpdateDiscountDto, Discount>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Discount, DiscountDto>();
        }
    }
}