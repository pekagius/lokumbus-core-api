// UpdateChatMessageDtoValidator.cs

using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;
using MongoDB.Bson;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update
{
    /// <summary>
    /// Validator for UpdateChatMessageDto using FluentValidation.
    /// </summary>
    public class UpdateChatMessageDtoValidator : AbstractValidator<UpdateChatMessageDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateChatMessageDtoValidator"/> class.
        /// </summary>
        public UpdateChatMessageDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeValidObjectId).WithMessage("Id must be a valid ObjectId.");

            RuleFor(x => x.Content)
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Content));

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid MessageStatus value.")
                .When(x => x.Status.HasValue);
        }

        private bool BeValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}