using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateCalendarDto unter Verwendung von FluentValidation.
    /// </summary>
    public class CreateCalendarDtoValidator : AbstractValidator<CreateCalendarDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der CreateCalendarDtoValidator-Klasse.
        /// </summary>
        public CreateCalendarDtoValidator()
        {
            // Validierung des OwnerId
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId ist erforderlich.")
                .Matches("^[a-fA-F0-9]{24}$").WithMessage("OwnerId muss eine gültige ObjectId sein.");
            
            // Validierung des OwnerType
            RuleFor(x => x.OwnerType)
                .IsInEnum().WithMessage("OwnerType muss ein gültiger Wert sein.");
            
            // Validierung des Namens
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name ist erforderlich.")
                .MaximumLength(100).WithMessage("Name darf maximal 100 Zeichen lang sein.");
            
            // Validierung der Beschreibung
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Beschreibung darf maximal 500 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Description));
            
            // Validierung der Zeitzone
            RuleFor(x => x.TimeZone)
                .MaximumLength(50).WithMessage("TimeZone darf maximal 50 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.TimeZone));
            
            // Validierung der Metadaten
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Count <= 1000)
                .WithMessage("Metadata darf maximal 1000 Einträge enthalten.");
            
            // Weitere Validierungsregeln können hier hinzugefügt werden.
        }
    }
}