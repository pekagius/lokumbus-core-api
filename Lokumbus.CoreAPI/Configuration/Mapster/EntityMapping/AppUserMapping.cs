using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models;
using Mapster;

namespace Lokumbus.CoreAPI.Configuration.Mapster.EntityMapping
{
    public class AppUserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
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
        }
    }
}