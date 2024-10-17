using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class PersonaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatePersonaDto, Persona>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            config.NewConfig<UpdatePersonaDto, Persona>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Persona, PersonaDto>();
        }
    }
}