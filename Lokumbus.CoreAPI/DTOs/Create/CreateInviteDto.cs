namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object für die Erstellung einer neuen Einladung (Invite).
    /// </summary>
    public class CreateInviteDto
    {
        /// <summary>
        /// Die E-Mail-Adresse, an die die Einladung gesendet werden soll.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Der eindeutige Code der Einladung.
        /// </summary>
        public string Code { get; set; }
    }
}