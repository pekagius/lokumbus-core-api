namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object für eine InterestRelation.
    /// </summary>
    public class InterestRelationDto
    {
        /// <summary>
        /// Die eindeutige Kennung der InterestRelation.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Die Kennung des ersten Interesses.
        /// </summary>
        public string InterestId { get; set; }

        /// <summary>
        /// Die Kennung des verwandten Interesses.
        /// </summary>
        public string RelatedInterestId { get; set; }

        /// <summary>
        /// Gewichtung der Beziehung.
        /// </summary>
        public string? Weight { get; set; }
    }
}