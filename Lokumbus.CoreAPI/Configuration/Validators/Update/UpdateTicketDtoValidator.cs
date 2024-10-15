using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators
{
    /// <summary>
    /// Validator for UpdateTicketDto using FluentValidation.
    /// </summary>
    public class UpdateTicketDtoValidator : AbstractValidator<UpdateTicketDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateTicketDtoValidator class.
        /// </summary>
        public UpdateTicketDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate Price
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.")
                .When(x => x.Price.HasValue);

            // Validate Quantity
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative number.")
                .When(x => x.Quantity.HasValue);

            // Validate StartDate
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("StartDate cannot be in the past.")
                .When(x => x.StartDate.HasValue);

            // Validate EndDate
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be after StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Validate UpdatedAt
            RuleFor(x => x.UpdatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedAt cannot be in the future.")
                .When(x => x.UpdatedAt.HasValue);

            // Validate IsActive
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.");

            // Validate Metadata
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Length <= 1000)
                .WithMessage("Metadata must not exceed 1000 characters.");
        }
    }
}