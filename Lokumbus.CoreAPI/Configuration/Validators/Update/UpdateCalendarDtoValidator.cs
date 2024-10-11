using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator für UpdateCalendarDto unter Verwendung von FluentValidation.
    /// </summary>
    public class UpdateCalendarDtoValidator : AbstractValidator<UpdateCalendarDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der UpdateCalendarDtoValidator-Klasse.
        /// </summary>
        public UpdateCalendarDtoValidator()
        {
            // Validierung des Namens
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name darf maximal 100 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Name));
            
            // Validierung der Beschreibung
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Beschreibung darf maximal 500 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Description));
            
            // Validierung der Zeitzone
            RuleFor(x => x.TimeZone)
                .MaximumLength(50).WithMessage("TimeZone darf maximal 50 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.TimeZone));
            
            // Validierung von IsPublic
            RuleFor(x => x.IsPublic)
                .NotNull().WithMessage("IsPublic darf nicht null sein.")
                .When(x => x.IsPublic.HasValue);
            
            // Validierung der Metadaten
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Count <= 1000)
                .WithMessage("Metadata darf maximal 1000 Einträge enthalten.");
            
            // Weitere Validierungsregeln können hier hinzugefügt werden.
        }
    }
}