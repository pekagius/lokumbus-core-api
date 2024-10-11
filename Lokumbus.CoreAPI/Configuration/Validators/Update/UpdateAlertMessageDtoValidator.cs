using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator für UpdateAlertMessageDto mit FluentValidation.
    /// </summary>
    public class UpdateAlertMessageDtoValidator : AbstractValidator<UpdateAlertMessageDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse <see cref="UpdateAlertMessageDtoValidator"/>.
        /// </summary>
        public UpdateAlertMessageDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Matches("^[a-fA-F0-9]{24}$").WithMessage("Id must be a valid ObjectId.");

            RuleFor(x => x.Content)
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Content));

            RuleFor(x => x.Status)
                .Must(status => Enum.IsDefined(typeof(MessageStatus), status))
                .WithMessage("Invalid status value.")
                .When(x => x.Status.HasValue);
        }
    }
}