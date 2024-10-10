using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateAuthDto using FluentValidation.
    /// </summary>
    public class UpdateAuthDtoValidator : AbstractValidator<UpdateAuthDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateAuthDtoValidator class.
        /// </summary>
        public UpdateAuthDtoValidator()
        {
            // Validate Provider
            RuleFor(x => x.Provider)
                .MaximumLength(50).WithMessage("Provider must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Provider));

            // Validate ProviderUserId
            RuleFor(x => x.ProviderUserId)
                .MaximumLength(50).WithMessage("ProviderUserId must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.ProviderUserId));

            // Validate ProviderAccessToken
            RuleFor(x => x.ProviderAccessToken)
                .MaximumLength(500).WithMessage("ProviderAccessToken must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.ProviderAccessToken));

            // Validate ProviderRefreshToken
            RuleFor(x => x.ProviderRefreshToken)
                .MaximumLength(500).WithMessage("ProviderRefreshToken must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.ProviderRefreshToken));

            // Validate IpAddress
            RuleFor(x => x.IpAddress)
                .MaximumLength(45).WithMessage("IpAddress must not exceed 45 characters.") // IPv6 max length
                .When(x => !string.IsNullOrEmpty(x.IpAddress));

            // Validate UserAgent
            RuleFor(x => x.UserAgent)
                .MaximumLength(500).WithMessage("UserAgent must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.UserAgent));

            // Validate DeviceId
            RuleFor(x => x.DeviceId)
                .MaximumLength(100).WithMessage("DeviceId must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeviceId));

            // Validate DeviceType
            RuleFor(x => x.DeviceType)
                .MaximumLength(50).WithMessage("DeviceType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeviceType));

            // Validate DeviceModel
            RuleFor(x => x.DeviceModel)
                .MaximumLength(100).WithMessage("DeviceModel must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeviceModel));

            // Validate DeviceOperatingSystem
            RuleFor(x => x.DeviceOperatingSystem)
                .MaximumLength(100).WithMessage("DeviceOperatingSystem must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeviceOperatingSystem));

            // Validate DeviceOperatingSystemVersion
            RuleFor(x => x.DeviceOperatingSystemVersion)
                .MaximumLength(50).WithMessage("DeviceOperatingSystemVersion must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.DeviceOperatingSystemVersion));

            // Validate Location
            RuleFor(x => x.Location)
                .MaximumLength(100).WithMessage("Location must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Location));

            // Validate Metadata
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Count <= 1000)
                .WithMessage("Metadata must not exceed 1000 entries.");
        }
    }
}