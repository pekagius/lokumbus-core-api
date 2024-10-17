using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class ReviewMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateReviewDto, Review>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            config.NewConfig<UpdateReviewDto, Review>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id);

            config.NewConfig<Review, ReviewDto>();
        }
    }
}