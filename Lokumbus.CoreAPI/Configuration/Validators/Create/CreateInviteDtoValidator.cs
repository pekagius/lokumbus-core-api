using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateInviteDto mit FluentValidation.
    /// </summary>
    public class CreateInviteDtoValidator : AbstractValidator<CreateInviteDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz des CreateInviteDtoValidator.
        /// </summary>
        public CreateInviteDtoValidator()
        {
            // Validate Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email ist erforderlich.")
                .EmailAddress().WithMessage("Ungültige E-Mail-Adresse.");

            // Validate Code
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code ist erforderlich.")
                .MaximumLength(50).WithMessage("Code darf maximal 50 Zeichen lang sein.");
        }
    }
}