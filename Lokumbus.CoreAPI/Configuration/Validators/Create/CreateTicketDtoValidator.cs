using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateTicketDto using FluentValidation.
    /// </summary>
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateTicketDtoValidator class.
        /// </summary>
        public CreateTicketDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate Price
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");

            // Validate Quantity
            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be a non-negative number.");

            // Validate StartDate
            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("StartDate cannot be in the past.")
                .When(x => x.StartDate.HasValue);

            // Validate EndDate
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be after StartDate.")
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

            // Validate Metadata
            RuleFor(x => x.Metadata)
                .Must(metadata => metadata == null || metadata.Length <= 1000)
                .WithMessage("Metadata must not exceed 1000 characters.");
        }
    }
}