using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class FriendshipMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateFriendshipDto, Friendship>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsAccepted, src => false)
                .Map(dest => dest.Metadata, src => new Dictionary<string, object>());

            config.NewConfig<UpdateFriendshipDto, Friendship>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.PersonaId)
                .Ignore(dest => dest.FriendPersonaId);

            config.NewConfig<Friendship, FriendshipDto>();
        }
    }
}