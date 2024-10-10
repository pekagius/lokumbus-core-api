using FluentValidation;
using Lokumbus.CoreAPI.DTOs.Update;
using MongoDB.Bson;

namespace Lokumbus.CoreAPI.Configuration.Validators.Update;

/// <summary>
/// Validator for UpdatePersonaDto using FluentValidation.
/// </summary>
public class UpdatePersonaDtoValidator : AbstractValidator<UpdatePersonaDto>
{
    /// <summary>
    /// Initializes a new instance of the UpdatePersonaDtoValidator class.
    /// </summary>
    public UpdatePersonaDtoValidator()
    {
        // Validate UserId
        RuleFor(x => x.UserId)
            .Must(IsValidObjectId).WithMessage("UserId must be a valid ObjectId.")
            .When(x => !string.IsNullOrEmpty(x.UserId));

        // Validate Name
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        // Validate Description
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        // Validate AvatarUrl
        RuleFor(x => x.AvatarUrl)
            .MaximumLength(200).WithMessage("AvatarUrl must not exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.AvatarUrl));

        // Validate CalendarIds
        RuleFor(x => x.CalendarIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All CalendarIds must be valid ObjectIds.");

        // Validate EventIds
        RuleFor(x => x.EventIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All EventIds must be valid ObjectIds.");

        // Validate TicketIds
        RuleFor(x => x.TicketIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All TicketIds must be valid ObjectIds.");

        // Validate InviteIds
        RuleFor(x => x.InviteIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All InviteIds must be valid ObjectIds.");

        // Validate FriendshipIds
        RuleFor(x => x.FriendshipIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All FriendshipIds must be valid ObjectIds.");

        // Validate ChatIds
        RuleFor(x => x.ChatIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All ChatIds must be valid ObjectIds.");

        // Validate ChatMessageIds
        RuleFor(x => x.ChatMessageIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All ChatMessageIds must be valid ObjectIds.");

        // Validate NotificationIds
        RuleFor(x => x.NotificationIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All NotificationIds must be valid ObjectIds.");

        // Validate ReviewIds
        RuleFor(x => x.ReviewIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All ReviewIds must be valid ObjectIds.");

        // Validate InterestIds
        RuleFor(x => x.InterestIds)
            .Must(ids => ids == null || ids.All(id => IsValidObjectId(id)))
            .WithMessage("All InterestIds must be valid ObjectIds.");

        // Validate UpdatedAt
        RuleFor(x => x.UpdatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedAt cannot be in the future.")
            .When(x => x.UpdatedAt.HasValue);

        // Validate IsActive
        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive cannot be null.")
            .When(x => x.IsActive.HasValue);

        // Validate Metadata
        RuleFor(x => x.Metadata)
            .Must(metadata => metadata == null || metadata.Count <= 1000)
            .WithMessage("Metadata must not exceed 1000 entries.");
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