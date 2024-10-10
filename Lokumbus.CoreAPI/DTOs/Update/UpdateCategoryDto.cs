namespace Lokumbus.CoreAPI.DTOs.Update;

/// <summary>
/// Data Transfer Object for updating an existing Category.
/// </summary>
public class UpdateCategoryDto
{
    /// <summary>
    /// The updated name of the Category.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The updated description of the Category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The updated icon representing the Category.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// The updated color code associated with the Category.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// The updated identifier of the parent Category, if any.
    /// </summary>
    public string? ParentCategoryId { get; set; }

    /// <summary>
    /// The updated URL to the Category's image.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The updated display order for the Category.
    /// </summary>
    public string? DisplayOrder { get; set; }

    /// <summary>
    /// The updated metadata associated with the Category.
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// The date and time when the Category was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the Category is active.
    /// </summary>
    public bool? IsActive { get; set; }
}