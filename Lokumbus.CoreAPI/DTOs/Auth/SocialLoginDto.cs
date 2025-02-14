using System.ComponentModel.DataAnnotations;

namespace Lokumbus.CoreAPI.DTOs.Auth;

public class SocialLoginDto
{
    /// <summary>
    /// Z.B. "google", "facebook", "apple".
    /// </summary>
    [Required]
    public string Provider { get; set; }

    /// <summary>
    /// ID Token (JWT), z. B. von Google oder Apple.
    /// Für Facebook meist nicht genutzt.
    /// </summary>
    public string? IdToken { get; set; }

    /// <summary>
    /// Access Token, z. B. Facebook oder Google (für Userinfo-API).
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// OPTIONAL: Falls Apple uns FullName/Email nur beim ersten Login gibt
    /// und du sie vom iOS-Client mitschicken willst.
    /// </summary>
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}