namespace Lokumbus.CoreAPI.DTOs.Update
{
    /// <summary>
    /// Data Transfer Object für die Aktualisierung einer bestehenden InterestRelation.
    /// </summary>
    public class UpdateInterestRelationDto
    {
        /// <summary>
        /// Die Kennung des ersten Interesses.
        /// </summary>
        public string? InterestId { get; set; }

        /// <summary>
        /// Die Kennung des verwandten Interesses.
        /// </summary>
        public string? RelatedInterestId { get; set; }

        /// <summary>
        /// Gewichtung der Beziehung.
        /// </summary>
        public string? Weight { get; set; }
    }
}