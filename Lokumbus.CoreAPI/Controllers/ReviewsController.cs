using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers;

/// <summary>
/// Controller für die Verwaltung von Reviews.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="ReviewsController"/>-Klasse.
    /// </summary>
    /// <param name="reviewService">Das Review-Service-Interface.</param>
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Ruft alle Reviews ab.
    /// </summary>
    /// <returns>Eine Sammlung von ReviewDto.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    /// <summary>
    /// Ruft ein spezifisches Review anhand der ID ab.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des Reviews.</param>
    /// <returns>Das angeforderte ReviewDto.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDto>> GetById(string id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null)
        {
            return NotFound(new { Message = $"Review with ID {id} was not found." });
        }
        return Ok(review);
    }

    /// <summary>
    /// Erstellt ein neues Review.
    /// </summary>
    /// <param name="createDto">Das DTO, das die Daten für das neue Review enthält.</param>
    /// <returns>Das erstellte ReviewDto.</returns>
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create([FromBody] CreateReviewDto createDto)
    {
        var createdReview = await _reviewService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdReview.Id }, createdReview);
    }

    /// <summary>
    /// Aktualisiert ein bestehendes Review.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des zu aktualisierenden Reviews.</param>
    /// <param name="updateDto">Das DTO, das die aktualisierten Daten enthält.</param>
    /// <returns>Ein IActionResult, das den Ausgang der Operation angibt.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateReviewDto updateDto)
    {
        try
        {
            await _reviewService.UpdateAsync(id, updateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Löscht ein Review anhand der ID.
    /// </summary>
    /// <param name="id">Die eindeutige Kennung des zu löschenden Reviews.</param>
    /// <returns>Ein IActionResult, das den Ausgang der Operation angibt.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _reviewService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}