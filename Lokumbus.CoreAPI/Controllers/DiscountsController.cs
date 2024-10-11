using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Discount resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        /// <summary>
        /// Initializes a new instance of the DiscountsController class.
        /// </summary>
        /// <param name="discountService">The Discount service instance.</param>
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// Retrieves all Discounts.
        /// </summary>
        /// <returns>A collection of DiscountDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> GetAll()
        {
            var discounts = await _discountService.GetAllAsync();
            return Ok(discounts);
        }

        /// <summary>
        /// Retrieves a specific Discount by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount.</param>
        /// <returns>The requested DiscountDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDto>> GetById(string id)
        {
            try
            {
                var discount = await _discountService.GetByIdAsync(id);
                return Ok(discount);
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the discount does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new Discount.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created DiscountDto.</returns>
        [HttpPost]
        public async Task<ActionResult<DiscountDto>> Create([FromBody] CreateDiscountDto createDto)
        {
            var createdDiscount = await _discountService.CreateAsync(createDto);
            // Return 201 Created with location header
            return CreatedAtAction(nameof(GetById), new { id = createdDiscount.Id }, createdDiscount);
        }

        /// <summary>
        /// Updates an existing Discount.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateDiscountDto updateDto)
        {
            try
            {
                await _discountService.UpdateAsync(id, updateDto);
                // Return 204 No Content on successful update
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the discount does not exist
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a Discount by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _discountService.DeleteAsync(id);
                // Return 204 No Content on successful deletion
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Return 404 Not Found if the discount does not exist
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}