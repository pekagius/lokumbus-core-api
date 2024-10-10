using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

/// <summary>
/// Controller for managing Category resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the CategoriesController class.
    /// </summary>
    /// <param name="categoryService">The Category service instance.</param>
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Retrieves all active Categories.
    /// </summary>
    /// <returns>A collection of active CategoryDto.</returns>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllActive()
    {
        var categories = await _categoryService.GetAllActiveAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Retrieves all Categories.
    /// </summary>
    /// <returns>A collection of all CategoryDto.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Retrieves a specific Category by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Category.</param>
    /// <returns>The requested CategoryDto.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(string id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the category does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Creates a new Category.
    /// </summary>
    /// <param name="createDto">The DTO containing creation data.</param>
    /// <returns>The created CategoryDto.</returns>
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto createDto)
    {
        var createdCategory = await _categoryService.CreateAsync(createDto);
        // Return 201 Created with location header
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    /// <summary>
    /// Updates an existing Category.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to update.</param>
    /// <param name="updateDto">The DTO containing update data.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateCategoryDto updateDto)
    {
        try
        {
            await _categoryService.UpdateAsync(id, updateDto);
            // Return 204 No Content on successful update
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the category does not exist
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a Category by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to delete.</param>
    /// <returns>An IActionResult indicating the outcome.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
            // Return 204 No Content on successful deletion
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 Not Found if the category does not exist
            return NotFound(new { Message = ex.Message });
        }
    }
}