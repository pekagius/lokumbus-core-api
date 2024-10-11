namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object für die Erstellung einer neuen InterestRelation.
    /// </summary>
    public class CreateInterestRelationDto
    {
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