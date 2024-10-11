namespace Lokumbus.CoreAPI.Models.Interfaces
{
    public interface IOrganizable
    {
        string? Name { get; set; }
        string? Description { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        Location? Location { get; set; }
        string? CategoryId { get; set; }
        bool? IsActive { get; set; }
    }
}