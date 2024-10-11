namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object für die Erstellung einer neuen Activity.
    /// </summary>
    public class CreateActivityDto
    {
        /// <summary>
        /// Der Benutzer-ID, dem die Activity gehört.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Der Name der Activity.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Eine kurze Beschreibung der Activity.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Das Startdatum und die Startzeit der Activity.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Das Enddatum und die Endzeit der Activity.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Die Dauer der Activity.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Die maximale Anzahl an Teilnehmern.
        /// </summary>
        public string? MaxParticipants { get; set; }

        /// <summary>
        /// Die minimale Anzahl an Teilnehmern.
        /// </summary>
        public string? MinParticipants { get; set; }

        /// <summary>
        /// Der Preis der Activity.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Die Währung des Preises.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Gibt an, ob die Activity öffentlich ist.
        /// </summary>
        public bool? IsPublic { get; set; }

        /// <summary>
        /// Gibt an, ob die Activity aktiv ist.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Der Typ der Activity.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Die Kategorie der Activity.
        /// </summary>
        public string? CategoryId { get; set; }

        /// <summary>
        /// Tags für die Activity.
        /// </summary>
        public string[]? Tags { get; set; }

        /// <summary>
        /// URLs zu Bildern der Activity.
        /// </summary>
        public string[]? Images { get; set; }

        /// <summary>
        /// URLs zu Videos der Activity.
        /// </summary>
        public string[]? Videos { get; set; }

        /// <summary>
        /// Die URL der Activity.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Die Ticket-URL der Activity.
        /// </summary>
        public string? TicketUrl { get; set; }

        /// <summary>
        /// Namen der Sponsoren.
        /// </summary>
        public string[]? SponsorNames { get; set; }

        /// <summary>
        /// Gibt an, ob Sponsoren sichtbar sind.
        /// </summary>
        public bool? IsSponsorsVisible { get; set; }

        /// <summary>
        /// Geschätzte Zeit in Minuten.
        /// </summary>
        public string? EstimatedTimeInMinutes { get; set; }

        /// <summary>
        /// Liste von Einrichtungen in der Activity.
        /// </summary>
        public string[]? Amenities { get; set; }

        /// <summary>
        /// Gibt an, ob die Activity im Freien stattfindet.
        /// </summary>
        public bool? IsOutdoor { get; set; }

        /// <summary>
        /// Gibt an, ob die Activity im Innenbereich stattfindet.
        /// </summary>
        public bool? IsIndoor { get; set; }

        /// <summary>
        /// Empfehlungen für die Activity.
        /// </summary>
        public string[]? Recommendations { get; set; }

        /// <summary>
        /// Warnungen für die Activity.
        /// </summary>
        public string[]? Warnings { get; set; }

        /// <summary>
        /// Schwierigkeitsgrad der Activity.
        /// </summary>
        public string? Difficulty { get; set; }

        /// <summary>
        /// Entfernung zur Activity.
        /// </summary>
        public double? Distance { get; set; }

        /// <summary>
        /// Einheit der Entfernung.
        /// </summary>
        public string? DistanceUnit { get; set; }

        /// <summary>
        /// Zusätzliche Anforderungen.
        /// </summary>
        public string[]? Requirements { get; set; }

        /// <summary>
        /// Sicherheitsanweisungen.
        /// </summary>
        public string[]? SafetyInstructions { get; set; }

        /// <summary>
        /// Regeln der Activity.
        /// </summary>
        public string[]? Rules { get; set; }

        /// <summary>
        /// Erlaubte Altersgruppe.
        /// </summary>
        public string? AgeRestriction { get; set; }

        /// <summary>
        /// Liste von Ausrüstung in der Activity.
        /// </summary>
        public string[]? Equipment { get; set; }

        /// <summary>
        /// Legt nahe, ob die Activity gesponsert ist.
        /// </summary>
        public bool? IsSponsored { get; set; }

        /// <summary>
        /// Zusätzliche Metadaten.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }
    }
}