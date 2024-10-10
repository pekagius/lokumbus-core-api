using System.Reflection;
using Lokumbus.CoreAPI.Configuration.Mapping.Converters;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;
using Mapster.Utils;

namespace Lokumbus.CoreAPI.Configuration.Mapping
{
    public class MapsterConfiguration
    {
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            // Schritt 1: Registrieren Sie den benutzerdefinierten Converter
            config.ScanInheritedTypes(Assembly.GetExecutingAssembly());
            config.Apply(new DictionaryStringObjectConverter());

            // Mapping von CreateAppUserDto zu AppUser
            config.NewConfig<CreateAppUserDto, AppUser>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.IsVerified, src => false);

            // Mapping von UpdateAppUserDto zu AppUser
            config.NewConfig<UpdateAppUserDto, AppUser>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.VerificationToken)
                .Ignore(dest => dest.VerificationTokenExpiresAt)
                .Ignore(dest => dest.ResetPasswordToken)
                .Ignore(dest => dest.ResetPasswordTokenExpiresAt)
                .Ignore(dest => dest.LastLoginIpAddress)
                .Ignore(dest => dest.LastLoginAt)
                .Ignore(dest => dest.Metadata)
                .Ignore(dest => dest.Password); // Passwort separat behandeln, falls nötig

            // Mapping von AppUser zu AppUserDto
            config.NewConfig<AppUser, AppUserDto>();

            // Mapping von CreateCategoryDto zu Category
            config.NewConfig<CreateCategoryDto, Category>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping von UpdateCategoryDto zu Category
            config.NewConfig<UpdateCategoryDto, Category>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Category zu CategoryDto
            config.NewConfig<Category, CategoryDto>();

            // Mapping von CreatePersonaDto zu Persona
            config.NewConfig<CreatePersonaDto, Persona>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping von UpdatePersonaDto zu Persona
            config.NewConfig<UpdatePersonaDto, Persona>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Persona zu PersonaDto
            config.NewConfig<Persona, PersonaDto>();

            // Mapping von CreateLocationDto zu Location
            config.NewConfig<CreateLocationDto, Location>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            // Mapping von UpdateLocationDto zu Location
            config.NewConfig<UpdateLocationDto, Location>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            // Mapping von Location zu LocationDto
            config.NewConfig<Location, LocationDto>();

            // Mapping von CreateAreaDto zu Area
            config.NewConfig<CreateAreaDto, Area>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping von UpdateAreaDto zu Area  
            config.NewConfig<UpdateAreaDto, Area>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Area zu AreaDto
            config.NewConfig<Area, AreaDto>();
        }
    }
}