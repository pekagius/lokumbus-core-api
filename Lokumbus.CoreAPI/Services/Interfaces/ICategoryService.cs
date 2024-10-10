using System.Collections.Generic;
using System.Threading.Tasks;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Defines the contract for Category service operations.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category.</param>
    /// <returns>The CategoryDto if found; otherwise, null.</returns>
    Task<CategoryDto> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves all active Categories.
    /// </summary>
    /// <returns>A collection of active CategoryDtos.</returns>
    Task<IEnumerable<CategoryDto>> GetAllActiveAsync();

    /// <summary>
    /// Retrieves all Categories.
    /// </summary>
    /// <returns>A collection of all CategoryDtos.</returns>
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    /// <summary>
    /// Creates a new Category.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created CategoryDto.</returns>
    Task<CategoryDto> CreateAsync(CreateCategoryDto createDto);

    /// <summary>
    /// Updates an existing Category.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    Task UpdateAsync(string id, UpdateCategoryDto updateDto);

    /// <summary>
    /// Deletes a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to delete.</param>
    Task DeleteAsync(string id);
}