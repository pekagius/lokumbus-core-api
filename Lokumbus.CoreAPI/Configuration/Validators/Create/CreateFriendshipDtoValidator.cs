using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateFriendshipDto unter Verwendung von FluentValidation.
    /// </summary>
    public class CreateFriendshipDtoValidator : AbstractValidator<CreateFriendshipDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der CreateFriendshipDtoValidator-Klasse.
        /// </summary>
        public CreateFriendshipDtoValidator()
        {
            // Validate PersonaId
            RuleFor(x => x.PersonaId)
                .NotEmpty().WithMessage("PersonaId ist erforderlich.")
                .Length(24).WithMessage("PersonaId muss genau 24 Zeichen lang sein."); // MongoDB ObjectId Länge
    
            // Validate FriendPersonaId
            RuleFor(x => x.FriendPersonaId)
                .NotEmpty().WithMessage("FriendPersonaId ist erforderlich.")
                .Length(24).WithMessage("FriendPersonaId muss genau 24 Zeichen lang sein."); // MongoDB ObjectId Länge
    
            // Sicherstellen, dass PersonaId und FriendPersonaId unterschiedlich sind
            RuleFor(x => x)
                .Must(x => x.PersonaId != x.FriendPersonaId)
                .WithMessage("PersonaId und FriendPersonaId dürfen nicht identisch sein.");
        }
    }
}