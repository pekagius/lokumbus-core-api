using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateSponsorshipDto using FluentValidation.
    /// </summary>
    public class CreateSponsorshipDtoValidator : AbstractValidator<CreateSponsorshipDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSponsorshipDtoValidator class.
        /// </summary>
        public CreateSponsorshipDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate Amount
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            
        }
    }
}