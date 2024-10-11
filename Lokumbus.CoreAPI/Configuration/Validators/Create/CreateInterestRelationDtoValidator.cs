using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator für CreateInterestRelationDto mit FluentValidation.
    /// </summary>
    public class CreateInterestRelationDtoValidator : AbstractValidator<CreateInterestRelationDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz des CreateInterestRelationDtoValidator.
        /// </summary>
        public CreateInterestRelationDtoValidator()
        {
            RuleFor(x => x.InterestId)
                .NotEmpty().WithMessage("InterestId ist erforderlich.")
                .Must(IsValidObjectId).WithMessage("InterestId muss eine gültige ObjectId sein.");

            RuleFor(x => x.RelatedInterestId)
                .NotEmpty().WithMessage("RelatedInterestId ist erforderlich.")
                .Must(IsValidObjectId).WithMessage("RelatedInterestId muss eine gültige ObjectId sein.");

            RuleFor(x => x.Weight)
                .MaximumLength(50).WithMessage("Weight darf maximal 50 Zeichen lang sein.")
                .When(x => !string.IsNullOrEmpty(x.Weight));
        }

        /// <summary>
        /// Validiert, ob die angegebene Zeichenkette eine gültige MongoDB ObjectId ist.
        /// </summary>
        /// <param name="id">Die Zeichenkette zur Validierung.</param>
        /// <returns>True, wenn gültig; andernfalls false.</returns>
        private bool IsValidObjectId(string id)
        {
            return MongoDB.Bson.ObjectId.TryParse(id, out _);
        }
    }
}