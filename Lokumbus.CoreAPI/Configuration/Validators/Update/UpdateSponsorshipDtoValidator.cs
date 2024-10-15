using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators
{
    /// <summary>
    /// Validator for UpdateSponsorshipDto using FluentValidation.
    /// </summary>
    public class UpdateSponsorshipDtoValidator : AbstractValidator<UpdateSponsorshipDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSponsorshipDtoValidator class.
        /// </summary>
        public UpdateSponsorshipDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate Amount
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .When(x => x.Amount.HasValue);

            // Validate UpdatedAt
            RuleFor(x => x.UpdatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedAt cannot be in the future.")
                .When(x => x.UpdatedAt.HasValue);

            // Validate IsActive
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.");
        }
    }
}