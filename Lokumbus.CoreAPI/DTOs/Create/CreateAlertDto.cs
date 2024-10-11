namespace Lokumbus.CoreAPI.DTOs.Create
{
    /// <summary>
    /// Data Transfer Object for creating a new Alert.
    /// </summary>
    public class CreateAlertDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public string? Level { get; set; }
        public DateTime? Timestamp { get; set; }
        public string[]? Tags { get; set; }
        public string? TargetUserId { get; set; }
        public string? TargetGroupId { get; set; }
        public string? TargetArea { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Radius { get; set; }
        public string[]? AffectedServices { get; set; }
        public string[]? AffectedActivities { get; set; }
        public string[]? AffectedLocations { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? RecurrencePattern { get; set; }
        public string[]? Actions { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? IconUrl { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
    }
}