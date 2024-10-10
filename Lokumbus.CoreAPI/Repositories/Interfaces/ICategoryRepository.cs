using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces;

/// <summary>
/// Defines the contract for Category repository operations.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category.</param>
    /// <returns>The Category if found; otherwise, null.</returns>
    Task<Category> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all active Categories.
    /// </summary>
    /// <returns>A collection of active Categories.</returns>
    Task<IEnumerable<Category>> GetAllActiveAsync();

    /// <summary>
    /// Retrieves all Categories.
    /// </summary>
    /// <returns>A collection of all Categories.</returns>
    Task<IEnumerable<Category>> GetAllAsync();

    /// <summary>
    /// Creates a new Category.
    /// </summary>
    /// <param name="category">The Category to create.</param>
    Task CreateAsync(Category category);

    /// <summary>
    /// Updates an existing Category.
    /// </summary>
    /// <param name="category">The Category with updated information.</param>
    Task UpdateAsync(Category category);

    /// <summary>
    /// Deletes a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to delete.</param>
    Task DeleteAsync(string id);
}