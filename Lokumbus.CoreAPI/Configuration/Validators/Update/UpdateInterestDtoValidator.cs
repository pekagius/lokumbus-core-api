using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;
using MongoDB.Bson;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateInterestDto using FluentValidation.
    /// </summary>
    public class UpdateInterestDtoValidator : AbstractValidator<UpdateInterestDto>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateInterestDtoValidator class.
        /// </summary>
        public UpdateInterestDtoValidator()
        {
            // Validate Name
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            // Validate Description
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validate CategoryId
            RuleFor(x => x.CategoryId)
                .MaximumLength(24).WithMessage("CategoryId must not exceed 24 characters.")
                .When(x => !string.IsNullOrEmpty(x.CategoryId));

            // Validate EventIds
            RuleFor(x => x.EventIds)
                .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
                .WithMessage("All EventIds must be valid ObjectIds.");

            // Validate UserIds
            RuleFor(x => x.UserIds)
                .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
                .WithMessage("All UserIds must be valid ObjectIds.");

            // Validate IsActive
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive cannot be null.");

            // Additional validation rules can be added here as needed.
        }

        /// <summary>
        /// Checks if the provided string is a valid MongoDB ObjectId.
        /// </summary>
        /// <param name="id">The string to validate.</param>
        /// <returns>True if valid; otherwise, false.</returns>
        private bool IsValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}