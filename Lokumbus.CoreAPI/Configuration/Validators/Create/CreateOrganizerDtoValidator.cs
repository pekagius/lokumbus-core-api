using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators
{
    /// <summary>
    /// Validator for CreateOrganizerDto using FluentValidation.
    /// </summary>
    public class CreateOrganizerDtoValidator : AbstractValidator<CreateOrganizerDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateOrganizerDtoValidator class.
        /// </summary>
        public CreateOrganizerDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            // Validate Phone
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

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

            // Additional validation rules can be added here as needed.
        }
    }
}