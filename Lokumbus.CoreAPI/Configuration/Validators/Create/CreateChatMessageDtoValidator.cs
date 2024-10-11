// CreateChatMessageDtoValidator.cs

using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Create;
using MongoDB.Bson;

namespace Lokumbus.CoreAPI.Configuration.Validators.Create
{
    /// <summary>
    /// Validator for CreateChatMessageDto using FluentValidation.
    /// </summary>
    public class CreateChatMessageDtoValidator : AbstractValidator<CreateChatMessageDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateChatMessageDtoValidator"/> class.
        /// </summary>
        public CreateChatMessageDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("SenderId is required.")
                .Must(BeValidObjectId).WithMessage("SenderId must be a valid ObjectId.");

            RuleFor(x => x.RecipientId)
                .NotEmpty().WithMessage("RecipientId is required.")
                .Must(BeValidObjectId).WithMessage("RecipientId must be a valid ObjectId.");

            RuleFor(x => x.ChatId)
                .Must(BeValidObjectId).WithMessage("ChatId must be a valid ObjectId.")
                .When(x => !string.IsNullOrEmpty(x.ChatId));
        }

        private bool BeValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}