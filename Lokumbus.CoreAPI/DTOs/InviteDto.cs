namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object für eine Einladung (Invite).
    /// </summary>
    public class InviteDto
    {
        /// <summary>
        /// Die eindeutige Kennung der Einladung.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Die E-Mail-Adresse, an die die Einladung gesendet wurde.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Der eindeutige Code der Einladung.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Das Datum und die Uhrzeit, wann die Einladung gesendet wurde.
        /// </summary>
        public DateTime? SentAt { get; set; }

        /// <summary>
        /// Das Datum und die Uhrzeit, wann die Einladung angenommen wurde.
        /// </summary>
        public DateTime? AcceptedAt { get; set; }
    }
}