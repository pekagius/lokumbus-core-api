using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateAlertDto using FluentValidation.
    /// </summary>
    public class UpdateAlertDtoValidator : AbstractValidator<UpdateAlertDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateAlertDtoValidator class.
        /// </summary>
        public UpdateAlertDtoValidator()
        {
            // Validate Title
            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Title));

            // Validate Message
            RuleFor(x => x.Message)
                .MaximumLength(500).WithMessage("Message must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Message));

            // Validate Type
            RuleFor(x => x.Type)
                .MaximumLength(50).WithMessage("Type must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Type));

            // Validate Level
            RuleFor(x => x.Level)
                .MaximumLength(50).WithMessage("Level must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Level));

            // Validate Timestamp
            RuleFor(x => x.Timestamp)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Timestamp cannot be in the future.")
                .When(x => x.Timestamp.HasValue);

            // Validate IsRead
            RuleFor(x => x.IsRead)
                .NotNull().WithMessage("IsRead cannot be null.")
                .When(x => x.IsRead.HasValue);

            // Validate IsDismissed
            RuleFor(x => x.IsDismissed)
                .NotNull().WithMessage("IsDismissed cannot be null.")
                .When(x => x.IsDismissed.HasValue);

            // Additional validation rules can be added here as needed.
        }
    }
}