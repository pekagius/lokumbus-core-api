using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateOrganizerDto using FluentValidation.
    /// </summary>
    public class UpdateOrganizerDtoValidator : AbstractValidator<UpdateOrganizerDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateOrganizerDtoValidator class.
        /// </summary>
        public UpdateOrganizerDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            // Validate Email
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            // Validate Phone
            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            // Validate Website
            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Website URL must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.Website));

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate LogoUrl
            RuleFor(x => x.LogoUrl)
                .MaximumLength(200).WithMessage("Logo URL must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.LogoUrl));

            // Validate AddressId
            RuleFor(x => x.AddressId)
                .MaximumLength(24).WithMessage("AddressId must not exceed 24 characters.")
                .When(x => !string.IsNullOrEmpty(x.AddressId));

            // Validate IsActive
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.")
                .When(x => x.IsActive.HasValue);

            // Validate Status
            RuleFor(x => x.Status)
                .MaximumLength(50).WithMessage("Status must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Status));

            // Additional validation rules can be added here as needed.
        }
    }
}