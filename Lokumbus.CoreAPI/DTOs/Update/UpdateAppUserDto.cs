namespace Lokumbus.CoreAPI.DTOs.Update;

/// <summary>
/// Data Transfer Object for updating an existing AppUser.
/// </summary>
public class UpdateAppUserDto
{
    /// <summary>
    /// Updated list of Persona IDs associated with the user.
    /// </summary>
    public List<string>? PersonaIds { get; set; }

    /// <summary>
    /// The updated username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The updated email address of the user.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The updated password for the user account.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The updated first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The updated last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The updated phone number of the user.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// The updated date of birth of the user.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// The updated gender of the user.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// The updated URL to the user's avatar image.
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// The updated biography of the user.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// The updated address ID associated with the user.
    /// </summary>
    public string? AddressId { get; set; }

    /// <summary>
    /// Indicates whether the user is active.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// Indicates whether the user's email is verified.
    /// </summary>
    public bool? IsVerified { get; set; }
}