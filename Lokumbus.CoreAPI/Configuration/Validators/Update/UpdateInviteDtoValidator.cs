using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator für UpdateInviteDto mit FluentValidation.
    /// </summary>
    public class UpdateInviteDtoValidator : AbstractValidator<UpdateInviteDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz des UpdateInviteDtoValidator.
        /// </summary>
        public UpdateInviteDtoValidator()
        {
            // Validate Email (optional)
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Ungültige E-Mail-Adresse.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            // Validate Code (optional)
            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("Code darf maximal 50 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Code));

            // Validate AcceptedAt (optional)
            RuleFor(x => x.AcceptedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("AcceptedAt darf nicht in der Zukunft liegen.")
                .When(x => x.AcceptedAt.HasValue);
        }
    }
}