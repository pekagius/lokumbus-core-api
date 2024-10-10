namespace Lokumbus.CoreAPI.DTOs;

/// <summary>
/// Data Transfer Object representing a Category.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// The unique identifier of the Category.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The name of the Category.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A brief description of the Category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The icon representing the Category.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// The color code associated with the Category.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// The identifier of the parent Category, if any.
    /// </summary>
    public string? ParentCategoryId { get; set; }

    /// <summary>
    /// The URL to the Category's image.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The display order for the Category.
    /// </summary>
    public string? DisplayOrder { get; set; }

    /// <summary>
    /// Additional metadata associated with the Category.
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// The date and time when the Category was created.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the Category was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the Category is active.
    /// </summary>
    public bool? IsActive { get; set; }
}