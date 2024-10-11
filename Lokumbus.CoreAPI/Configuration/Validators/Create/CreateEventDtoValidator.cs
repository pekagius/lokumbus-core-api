using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateEventDto using FluentValidation.
    /// </summary>
    public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateEventDtoValidator class.
        /// </summary>
        public CreateEventDtoValidator()
        {
            // Validate Title
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate StartDateTime
            RuleFor(x => x.StartDateTime)
                .NotEmpty().WithMessage("StartDateTime is required.")
                .LessThanOrEqualTo(x => x.EndDateTime).WithMessage("StartDateTime must be before EndDateTime.");

            // Validate EndDateTime
            RuleFor(x => x.EndDateTime)
                .NotEmpty().WithMessage("EndDateTime is required.")
                .GreaterThan(x => x.StartDateTime).WithMessage("EndDateTime must be after StartDateTime.");

            // Additional validation rules...

            // Validate Price
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.")
                .When(x => x.Price.HasValue);

            // Validate Currency
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a valid 3-letter ISO code.")
                .When(x => !string.IsNullOrEmpty(x.Currency));

            // Validate Metadata
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Count <= 1000)
                .WithMessage("Metadata must not exceed 1000 entries.")
                .When(x => x.Metadata != null);
        }
    }
}