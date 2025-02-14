using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;
using System;

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
                .NotEmpty().WithMessage("UserId ist erforderlich.")
                .MaximumLength(24).WithMessage("UserId darf maximal 24 Zeichen lang sein.");

            // Validierung des Namens
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name ist erforderlich.")
                .MaximumLength(100).WithMessage("Name darf maximal 100 Zeichen lang sein.");

            // Validierung der Beschreibung
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Beschreibung darf maximal 500 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validierung des Start- und Enddatums
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("StartDate muss vor oder gleich EndDate sein.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate muss nach oder gleich StartDate sein.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Validierung der Dauer in Minuten
            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("DurationMinutes muss positiv sein.")
                .When(x => x.DurationMinutes.HasValue);

            // Weitere Validierungen
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price muss eine nicht-negative Zahl sein.");

            RuleFor(x => x.Currency)
                .MaximumLength(10).WithMessage("Currency darf maximal 10 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Currency));

            RuleFor(x => x.CategoryId)
                .MaximumLength(24).WithMessage("CategoryId darf maximal 24 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.CategoryId));

            // Validierung von Tags
            RuleFor(x => x.Tags)
                .Must(tags => tags == null || tags.Length <= 10)
                .WithMessage("Anzahl der Tags darf maximal 10 betragen.");

            // Validierung von URLs
            RuleFor(x => x.Url)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Url muss eine gültige URI sein.");

            RuleFor(x => x.TicketUrl)
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("TicketUrl muss eine gültige URI sein.");
        }
    }
}