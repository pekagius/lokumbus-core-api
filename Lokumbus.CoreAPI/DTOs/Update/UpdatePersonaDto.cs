using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs.Update;

/// <summary>
/// Data Transfer Object for updating an existing Persona.
/// </summary>
public class UpdatePersonaDto
{
    /// <summary>
    /// The updated unique identifier of the AppUser to which the Persona belongs.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The updated name of the Persona.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The updated description of the Persona.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The updated URL to the Persona's avatar image.
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// Updated list of Calendar IDs associated with the Persona.
    /// </summary>
    public List<string>? CalendarIds { get; set; }

    /// <summary>
    /// Updated list of Event IDs associated with the Persona.
    /// </summary>
    public List<string>? EventIds { get; set; }

    /// <summary>
    /// Updated list of Ticket IDs associated with the Persona.
    /// </summary>
    public List<string>? TicketIds { get; set; }

    /// <summary>
    /// Updated list of Invite IDs associated with the Persona.
    /// </summary>
    public List<string>? InviteIds { get; set; }

    /// <summary>
    /// Updated list of Friendship IDs associated with the Persona.
    /// </summary>
    public List<string>? FriendshipIds { get; set; }

    /// <summary>
    /// Updated list of Chat IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatIds { get; set; }

    /// <summary>
    /// Updated list of ChatMessage IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatMessageIds { get; set; }

    /// <summary>
    /// Updated list of Notification IDs associated with the Persona.
    /// </summary>
    public List<string>? NotificationIds { get; set; }

    /// <summary>
    /// Updated list of Review IDs associated with the Persona.
    /// </summary>
    public List<string>? ReviewIds { get; set; }

    /// <summary>
    /// Updated list of Interest IDs associated with the Persona.
    /// </summary>
    public List<string>? InterestIds { get; set; }

    /// <summary>
    /// The date and time when the Persona was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the Persona is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Metadata associated with the Persona.
    /// </summary>
    public List<MetaEntry>? Metadata { get; set; }
}