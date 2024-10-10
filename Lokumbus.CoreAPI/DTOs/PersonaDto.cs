namespace Lokumbus.CoreAPI.DTOs;

/// <summary>
/// Data Transfer Object representing a Persona.
/// </summary>
public class PersonaDto
{
    /// <summary>
    /// The unique identifier of the Persona.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The unique identifier of the AppUser to which the Persona belongs.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// The name of the Persona.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A brief description of the Persona.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The URL to the Persona's avatar image.
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// List of Calendar IDs associated with the Persona.
    /// </summary>
    public List<string>? CalendarIds { get; set; }

    /// <summary>
    /// List of Event IDs associated with the Persona.
    /// </summary>
    public List<string>? EventIds { get; set; }

    /// <summary>
    /// List of Ticket IDs associated with the Persona.
    /// </summary>
    public List<string>? TicketIds { get; set; }

    /// <summary>
    /// List of Invite IDs associated with the Persona.
    /// </summary>
    public List<string>? InviteIds { get; set; }

    /// <summary>
    /// List of Friendship IDs associated with the Persona.
    /// </summary>
    public List<string>? FriendshipIds { get; set; }

    /// <summary>
    /// List of Chat IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatIds { get; set; }

    /// <summary>
    /// List of ChatMessage IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatMessageIds { get; set; }

    /// <summary>
    /// List of Notification IDs associated with the Persona.
    /// </summary>
    public List<string>? NotificationIds { get; set; }

    /// <summary>
    /// List of Review IDs associated with the Persona.
    /// </summary>
    public List<string>? ReviewIds { get; set; }

    /// <summary>
    /// List of Interest IDs associated with the Persona.
    /// </summary>
    public List<string>? InterestIds { get; set; }

    /// <summary>
    /// The date and time when the Persona was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

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
    public Dictionary<string, object>? Metadata { get; set; }
}
