namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object für die Aktualisierung einer bestehenden Einladung (Invite).
    /// </summary>
    public class UpdateInviteDto
    {
        /// <summary>
        /// Die aktualisierte E-Mail-Adresse, an die die Einladung gesendet wurde.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Der aktualisierte eindeutige Code der Einladung.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Das Datum und die Uhrzeit, wann die Einladung angenommen wurde.
        /// </summary>
        public DateTime? AcceptedAt { get; set; }
    }
}