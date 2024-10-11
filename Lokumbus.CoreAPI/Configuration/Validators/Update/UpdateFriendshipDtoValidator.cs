using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator für UpdateFriendshipDto unter Verwendung von FluentValidation.
    /// </summary>
    public class UpdateFriendshipDtoValidator : AbstractValidator<UpdateFriendshipDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der UpdateFriendshipDtoValidator-Klasse.
        /// </summary>
        public UpdateFriendshipDtoValidator()
        {
            // Validate IsAccepted
            RuleFor(x => x.IsAccepted)
                .NotNull().WithMessage("IsAccepted darf nicht null sein.");
    
            // Optional: Weitere Validierungen für Metadaten
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Count <= 1000)
                .WithMessage("Metadata darf nicht mehr als 1000 Einträge enthalten.");
        }
    }
}