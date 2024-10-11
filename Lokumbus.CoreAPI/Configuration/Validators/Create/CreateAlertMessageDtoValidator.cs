using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateAlertMessageDto mit FluentValidation.
    /// </summary>
    public class CreateAlertMessageDtoValidator : AbstractValidator<CreateAlertMessageDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse <see cref="CreateAlertMessageDtoValidator"/>.
        /// </summary>
        public CreateAlertMessageDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

            RuleFor(x => x.SystemId)
                .Matches("^[a-fA-F0-9]{24}$").WithMessage("SystemId must be a valid ObjectId.")
                .When(x => !string.IsNullOrEmpty(x.SystemId));

// Weitere Validierungen können hier hinzugefügt werden.
        }
    }
}