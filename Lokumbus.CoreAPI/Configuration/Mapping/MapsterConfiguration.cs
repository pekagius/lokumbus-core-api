using System.Reflection;
using Lokumbus.CoreAPI.Configuration.Mapping.Converters;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Mapster;
using Mapster.Utils;

namespace Lokumbus.CoreAPI.Configuration.Mapping
{
    public class MapsterConfiguration
    {
        
        public static void RegisterMappings(TypeAdapterConfig config)
        {
            config.ScanInheritedTypes(Assembly.GetExecutingAssembly());
            config.Apply(new DictionaryStringObjectConverter());

            config.NewConfig<CreateAppUserDto, AppUser>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.IsVerified, src => false)
                .Map(dest => dest.PasswordHash, src => src.Password);

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
            
            config.NewConfig<AppUser, AppUserDto>();

            config.NewConfig<CreateCategoryDto, Category>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            config.NewConfig<UpdateCategoryDto, Category>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<Category, CategoryDto>();

            config.NewConfig<CreatePersonaDto, Persona>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            config.NewConfig<UpdatePersonaDto, Persona>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Persona, PersonaDto>();

            config.NewConfig<CreateLocationDto, Location>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);
            
            config.NewConfig<UpdateLocationDto, Location>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            config.NewConfig<Location, LocationDto>();
            
            config.NewConfig<CreateAuthDto, Auth>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => src.IsActive ?? true);

            config.NewConfig<UpdateAuthDto, Auth>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            config.NewConfig<Auth, AuthDto>();

            config.NewConfig<CreateActivityDto, Activity>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            config.NewConfig<UpdateActivityDto, Activity>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Activity, ActivityDto>();

            config.NewConfig<CreateAlertDto, Alert>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);
            
            config.NewConfig<UpdateAlertDto, Alert>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<Alert, AlertDto>();

            config.NewConfig<CreateCalendarDto, Calendar>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);
            
            config.NewConfig<UpdateCalendarDto, Calendar>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<Calendar, CalendarDto>();

            config.NewConfig<CreateEventDto, Event>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);
            
            config.NewConfig<UpdateEventDto, Event>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<Event, EventDto>();
            
            config.NewConfig<CreateCalendarEventAttendeeDto, CalendarEventAttendee>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);
            
            config.NewConfig<UpdateCalendarEventAttendeeDto, CalendarEventAttendee>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<CalendarEventAttendee, CalendarEventAttendeeDto>();
            
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
            
            config.NewConfig<CreateAlertMessageDto, AlertMessage>()
                .Map(dest => dest.Status, src => MessageStatus.Sent) // Example initialization
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

            config.NewConfig<CreateDiscountDto, Discount>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);
            
            config.NewConfig<UpdateDiscountDto, Discount>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Discount, DiscountDto>();
            
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
            
            config.NewConfig<CreateInterestDto, Interest>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => src.IsActive ?? true);

            config.NewConfig<UpdateInterestDto, Interest>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.Metadata);
            
            config.NewConfig<Interest, InterestDto>();
            
            config.NewConfig<CreateInterestRelationDto, InterestRelation>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);
            
            config.NewConfig<UpdateInterestRelationDto, InterestRelation>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<InterestRelation, InterestRelationDto>();
            
            config.NewConfig<CreateInviteDto, Invite>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);
            
            config.NewConfig<UpdateInviteDto, Invite>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<Invite, InviteDto>();
            
   
            config.NewConfig<CreateOrganizerDto, Organizer>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.IsVerified, src => false);

        
            config.NewConfig<UpdateOrganizerDto, Organizer>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);
            
            config.NewConfig<Organizer, OrganizerDto>();
        }
    }
}