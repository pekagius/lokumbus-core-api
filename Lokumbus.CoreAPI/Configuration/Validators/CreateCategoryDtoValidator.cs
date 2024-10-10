using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators;

/// <summary>
/// Validator for CreateCategoryDto using FluentValidation.
/// </summary>
public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    /// <summary>
    /// Initializes a new instance of the CreateCategoryDtoValidator class.
    /// </summary>
    public CreateCategoryDtoValidator()
    {
        // Validate Name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        // Validate Description
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        // Validate Icon
        RuleFor(x => x.Icon)
            .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Icon));

        // Validate Color
        RuleFor(x => x.Color)
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Color));

        // Validate ParentCategoryId
        RuleFor(x => x.ParentCategoryId)
            .MaximumLength(50).WithMessage("ParentCategoryId must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Color));

        // Validate ImageUrl
        RuleFor(x => x.ImageUrl)
            .MaximumLength(200).WithMessage("ImageUrl must not exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl));

        // Validate DisplayOrder
        RuleFor(x => x.DisplayOrder)
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.DisplayOrder));

        // Validate Metadata
        RuleFor(x => x.Metadata)
            .MaximumLength(1000).WithMessage("Metadata must not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Metadata));

        // Additional validation rules can be added here as needed.
    }
}