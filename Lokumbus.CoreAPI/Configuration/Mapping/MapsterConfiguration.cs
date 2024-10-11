using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;


namespace Lokumbus.CoreAPI.Configuration.Mapping
{
    /// <summary>
    /// Configures Mapster type mappings.
    /// </summary>
    public class MapsterConfiguration
    {
        /// <summary>
        /// Registers all type mappings with the provided Mapster configuration.
        /// </summary>
        /// <param name="config">The Mapster configuration to register mappings with.</param>
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            // Mapping von CreateAppUserDto zu AppUser
            config.NewConfig<CreateAppUserDto, AppUser>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.IsVerified, src => false)
                .Map(dest => dest.PasswordHash, src => src.Password); 

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
                .Ignore(dest => dest.PasswordHash);

            // Mapping von AppUser zu AppUserDto
            config.NewConfig<AppUser, AppUserDto>();

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

            // Mapping für Auth

            // Mapping von CreateAuthDto zu Auth
            config.NewConfig<CreateAuthDto, Auth>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => src.IsActive ?? true);

            // Mapping von UpdateAuthDto zu Auth
            config.NewConfig<UpdateAuthDto, Auth>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            // Mapping von Auth zu AuthDto
            config.NewConfig<Auth, AuthDto>();
            
            // Mapping von CreateActivityDto zu Activity
            config.NewConfig<CreateActivityDto, Activity>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping von UpdateActivityDto zu Activity
            config.NewConfig<UpdateActivityDto, Activity>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Activity zu ActivityDto
            config.NewConfig<Activity, ActivityDto>();
            
            // Mapping von CreateAlertDto zu Alert
            config.NewConfig<CreateAlertDto, Alert>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsDismissed, src => false);

            // Mapping von UpdateAlertDto zu Alert
            config.NewConfig<UpdateAlertDto, Alert>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Alert zu AlertDto
            config.NewConfig<Alert, AlertDto>();
            
            // Mapping von UpdateCalendarDto zu Calendar
            config.NewConfig<CreateCalendarDto, Calendar>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);
            
            // Mapping von UpdateCalendarDto zu Calendar
            config.NewConfig<UpdateCalendarDto, Calendar>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping von Calendar zu CalendarDto
            config.NewConfig<Calendar, CalendarDto>();


        }
    }
}