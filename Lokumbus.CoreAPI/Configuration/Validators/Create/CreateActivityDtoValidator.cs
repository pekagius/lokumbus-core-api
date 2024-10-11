using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateActivityDto unter Verwendung von FluentValidation.
    /// </summary>
    public class CreateActivityDtoValidator : AbstractValidator<CreateActivityDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der CreateActivityDtoValidator Klasse.
        /// </summary>
        public CreateActivityDtoValidator()
        {
            // Validierung des UserId
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MaximumLength(24).WithMessage("UserId cannot exceed 24 characters.");

            // Validierung des Namens
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validierung der Beschreibung
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validierung der Start- und Enddaten
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("StartDate must be before or equal to EndDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be after or equal to StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Validierung der Dauer
            RuleFor(x => x.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be positive.")
                .When(x => x.Duration.HasValue);

            // Weitere Validierungen
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number.");

            RuleFor(x => x.Currency)
                .MaximumLength(10).WithMessage("Currency must not exceed 10 characters.")
                .When(x => !string.IsNullOrEmpty(x.Currency));

            RuleFor(x => x.CategoryId)
                .MaximumLength(24).WithMessage("CategoryId must not exceed 24 characters.")
                .When(x => !string.IsNullOrEmpty(x.CategoryId));

            // Validierung von Tags
            RuleFor(x => x.Tags)
                .Must(tags => tags == null || tags.Length <= 10)
                .WithMessage("Number of tags must not exceed 10.");

            // Validierung von URLs
            RuleFor(x => x.Url)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Url must be a valid URI.");

            RuleFor(x => x.TicketUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("TicketUrl must be a valid URI.");
        }
    }
}