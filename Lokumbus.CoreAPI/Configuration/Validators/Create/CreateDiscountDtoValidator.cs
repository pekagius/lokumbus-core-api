using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateDiscountDto using FluentValidation.
    /// </summary>
    public class CreateDiscountDtoValidator : AbstractValidator<CreateDiscountDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateDiscountDtoValidator class.
        /// </summary>
        public CreateDiscountDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate Amount
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("Amount must be a non-negative value.");

            // Validate Code
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

            // Validate StartDate
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("StartDate must be before or equal to EndDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Validate EndDate
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be after or equal to StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Additional validation rules can be added here as needed.
        }
    }
}