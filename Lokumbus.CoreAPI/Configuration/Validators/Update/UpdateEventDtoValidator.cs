using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateEventDto using FluentValidation.
    /// </summary>
    public class UpdateEventDtoValidator : AbstractValidator<UpdateEventDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateEventDtoValidator class.
        /// </summary>
        public UpdateEventDtoValidator()
        {
            // Validate Title
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Title));

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate StartDateTime and EndDateTime
            RuleFor(x => x.StartDateTime)
                .LessThanOrEqualTo(x => x.EndDateTime).WithMessage("StartDateTime must be before EndDateTime.")
                .When(x => x.StartDateTime.HasValue && x.EndDateTime.HasValue);

            RuleFor(x => x.EndDateTime)
                .GreaterThan(x => x.StartDateTime).WithMessage("EndDateTime must be after StartDateTime.")
                .When(x => x.StartDateTime.HasValue && x.EndDateTime.HasValue);

            // Validate Price
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.")
                .When(x => x.Price.HasValue);

            // Validate Currency
            RuleFor(x => x.Currency)
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