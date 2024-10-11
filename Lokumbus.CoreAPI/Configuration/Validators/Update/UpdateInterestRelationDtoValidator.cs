using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator für UpdateInterestRelationDto mit FluentValidation.
    /// </summary>
    public class UpdateInterestRelationDtoValidator : AbstractValidator<UpdateInterestRelationDto>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz des UpdateInterestRelationDtoValidator.
        /// </summary>
        public UpdateInterestRelationDtoValidator()
        {
            RuleFor(x => x.InterestId)
                .Must(IsValidObjectId).WithMessage("InterestId muss eine gültige ObjectId sein.")
                .When(x => !string.IsNullOrEmpty(x.InterestId));

            RuleFor(x => x.RelatedInterestId)
                .Must(IsValidObjectId).WithMessage("RelatedInterestId muss eine gültige ObjectId sein.")
                .When(x => !string.IsNullOrEmpty(x.RelatedInterestId));

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