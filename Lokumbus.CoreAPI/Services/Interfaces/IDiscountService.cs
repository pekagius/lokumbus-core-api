using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Discount service operations.
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Retrieves a Discount by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount.</param>
        /// <returns>The DiscountDto if found; otherwise, null.</returns>
        Task<DiscountDto> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Discounts.
        /// </summary>
        /// <returns>A collection of all DiscountDtos.</returns>
        Task<IEnumerable<DiscountDto>> GetAllAsync();

        /// <summary>
        /// Creates a new Discount.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created DiscountDto.</returns>
        Task<DiscountDto> CreateAsync(CreateDiscountDto createDto);

        /// <summary>
        /// Updates an existing Discount.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        Task UpdateAsync(string id, UpdateDiscountDto updateDto);

        /// <summary>
        /// Deletes a Discount by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount to delete.</param>
        Task DeleteAsync(string id);
    }
}