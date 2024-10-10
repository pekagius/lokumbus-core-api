namespace Lokumbus.CoreAPI.DTOs.Create;

/// <summary>
/// Data Transfer Object for creating a new Persona.
/// </summary>
public class CreatePersonaDto
{
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
    public List<string>? CalendarIds { get; set; } = new();

    /// <summary>
    /// List of Event IDs associated with the Persona.
    /// </summary>
    public List<string>? EventIds { get; set; } = new();

    /// <summary>
    /// List of Ticket IDs associated with the Persona.
    /// </summary>
    public List<string>? TicketIds { get; set; } = new();

    /// <summary>
    /// List of Invite IDs associated with the Persona.
    /// </summary>
    public List<string>? InviteIds { get; set; } = new();

    /// <summary>
    /// List of Friendship IDs associated with the Persona.
    /// </summary>
    public List<string>? FriendshipIds { get; set; } = new();

    /// <summary>
    /// List of Chat IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatIds { get; set; } = new();

    /// <summary>
    /// List of ChatMessage IDs associated with the Persona.
    /// </summary>
    public List<string>? ChatMessageIds { get; set; } = new();

    /// <summary>
    /// List of Notification IDs associated with the Persona.
    /// </summary>
    public List<string>? NotificationIds { get; set; } = new();

    /// <summary>
    /// List of Review IDs associated with the Persona.
    /// </summary>
    public List<string>? ReviewIds { get; set; } = new();

    /// <summary>
    /// List of Interest IDs associated with the Persona.
    /// </summary>
    public List<string>? InterestIds { get; set; } = new();

    /// <summary>
    /// The date and time when the Persona was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Indicates whether the Persona is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Metadata associated with the Persona.
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }
}