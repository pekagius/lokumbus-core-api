using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateAlertDto using FluentValidation.
    /// </summary>
    public class CreateAlertDtoValidator : AbstractValidator<CreateAlertDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateAlertDtoValidator class.
        /// </summary>
        public CreateAlertDtoValidator()
        {
            // Validate Title
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            // Validate Message
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MaximumLength(500).WithMessage("Message must not exceed 500 characters.");

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

            // Additional validation rules can be added here as needed.
        }
    }
}