namespace Lokumbus.CoreAPI.DTOs.Create;

/// <summary>
/// Data Transfer Object for creating a new AppUser.
/// </summary>
public class CreateAppUserDto
{
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
    /// The password for the user account.
    /// </summary>
    public string? Password { get; set; }

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
}