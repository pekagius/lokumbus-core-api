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

            // Mapping from CreateAppUserDto to AppUser
            config.NewConfig<CreateAppUserDto, AppUser>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.IsVerified, src => false)
                .Map(dest => dest.PasswordHash, src => src.Password);

            // Mapping from UpdateAppUserDto to AppUser
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

            // Mapping from AppUser to AppUserDto
            config.NewConfig<AppUser, AppUserDto>();

            // Mapping from CreateCategoryDto to Category
            config.NewConfig<CreateCategoryDto, Category>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping from UpdateCategoryDto to Category
            config.NewConfig<UpdateCategoryDto, Category>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Category to CategoryDto
            config.NewConfig<Category, CategoryDto>();

            // Mapping from CreatePersonaDto to Persona
            config.NewConfig<CreatePersonaDto, Persona>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping from UpdatePersonaDto to Persona
            config.NewConfig<UpdatePersonaDto, Persona>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Persona to PersonaDto
            config.NewConfig<Persona, PersonaDto>();

            // Mapping from CreateLocationDto to Location
            config.NewConfig<CreateLocationDto, Location>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            // Mapping from UpdateLocationDto to Location
            config.NewConfig<UpdateLocationDto, Location>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            // Mapping from Location to LocationDto
            config.NewConfig<Location, LocationDto>();

            // Mapping for Auth

            // Mapping from CreateAuthDto to Auth
            config.NewConfig<CreateAuthDto, Auth>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => src.IsActive ?? true);

            // Mapping from UpdateAuthDto to Auth
            config.NewConfig<UpdateAuthDto, Auth>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UserId);

            // Mapping from Auth to AuthDto
            config.NewConfig<Auth, AuthDto>();

            // Mapping from CreateActivityDto to Activity
            config.NewConfig<CreateActivityDto, Activity>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping from UpdateActivityDto to Activity
            config.NewConfig<UpdateActivityDto, Activity>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Activity to ActivityDto
            config.NewConfig<Activity, ActivityDto>();

            // Mapping from CreateAlertDto to Alert
            config.NewConfig<CreateAlertDto, Alert>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            // Mapping from UpdateAlertDto to Alert
            config.NewConfig<UpdateAlertDto, Alert>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Alert to AlertDto
            config.NewConfig<Alert, AlertDto>();

            // Mapping from CreateCalendarDto to Calendar
            config.NewConfig<CreateCalendarDto, Calendar>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            // Mapping from UpdateCalendarDto to Calendar
            config.NewConfig<UpdateCalendarDto, Calendar>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Calendar to CalendarDto
            config.NewConfig<Calendar, CalendarDto>();

            // Mapping from CreateEventDto to Event
            config.NewConfig<CreateEventDto, Event>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping from UpdateEventDto to Event
            config.NewConfig<UpdateEventDto, Event>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from Event to EventDto
            config.NewConfig<Event, EventDto>();

            // Mapping from CreateCalendarEventAttendeeDto to CalendarEventAttendee
            config.NewConfig<CreateCalendarEventAttendeeDto, CalendarEventAttendee>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.IsActive, src => true);

            // Mapping from UpdateCalendarEventAttendeeDto to CalendarEventAttendee
            config.NewConfig<UpdateCalendarEventAttendeeDto, CalendarEventAttendee>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt);

            // Mapping from CalendarEventAttendee to CalendarEventAttendeeDto
            config.NewConfig<CalendarEventAttendee, CalendarEventAttendeeDto>();

            // Mapping from CreateChatMessageDto to ChatMessage
            config.NewConfig<CreateChatMessageDto, ChatMessage>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .Map(dest => dest.SentAt, src => DateTime.UtcNow)
                .Map(dest => dest.Status, src => MessageStatus.Sent);

            // Mapping from UpdateChatMessageDto to ChatMessage
            config.NewConfig<UpdateChatMessageDto, ChatMessage>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.SentAt)
                .Ignore(dest => dest.DeliveredAt)
                .Ignore(dest => dest.ReadAt)
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow);

            // Mapping from ChatMessage to ChatMessageDto
            config.NewConfig<ChatMessage, ChatMessageDto>();


            // Mapping from CreateAlertMessageDto to AlertMessage
            config.NewConfig<CreateAlertMessageDto, AlertMessage>()
                .Map(dest => dest.Status, src => MessageStatus.Sent) // Example initialization
                .Map(dest => dest.SentAt, src => DateTime.UtcNow)
                .Map(dest => dest.SystemId, src => src.SystemId);

            // Mapping from UpdateAlertMessageDto to AlertMessage
            config.NewConfig<UpdateAlertMessageDto, AlertMessage>()
                .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.SentAt)
                .Ignore(dest => dest.DeliveredAt)
                .Ignore(dest => dest.ReadAt)
                .Ignore(dest => dest.SystemId);

            // Mapping from AlertMessage to AlertMessageDto
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
        }
    }
}