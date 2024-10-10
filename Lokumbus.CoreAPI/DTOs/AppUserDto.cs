namespace Lokumbus.CoreAPI.DTOs;

/// <summary>
/// Data Transfer Object representing an AppUser.
/// </summary>
public class AppUserDto
{
    /// <summary>
    /// The unique identifier of the AppUser.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// List of Persona IDs associated with the user.
    /// </summary>
    public List<string>? PersonaIds { get; set; }

    /// <summary>
    /// The username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The phone number of the user.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// The date of birth of the user.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// The gender of the user.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// The URL to the user's avatar image.
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// A short biography of the user.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The address ID associated with the user.
    /// </summary>
    public string? AddressId { get; set; }

    /// <summary>
    /// The date and time when the user was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the user was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the user is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Indicates whether the user's email is verified.
    /// </summary>
    public bool? IsVerified { get; set; }

    // Additional fields can be added here as needed.
}