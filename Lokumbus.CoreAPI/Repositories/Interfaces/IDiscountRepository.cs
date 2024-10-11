using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for Discount repository operations.
    /// </summary>
    public interface IDiscountRepository
    {
        /// <summary>
        /// Retrieves a Discount by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount.</param>
        /// <returns>The Discount if found; otherwise, null.</returns>
        Task<Discount?> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all Discounts.
        /// </summary>
        /// <returns>A collection of all Discounts.</returns>
        Task<IEnumerable<Discount>> GetAllAsync();

        /// <summary>
        /// Creates a new Discount.
        /// </summary>
        /// <param name="discount">The Discount to create.</param>
        Task CreateAsync(Discount discount);

        /// <summary>
        /// Updates an existing Discount.
        /// </summary>
        /// <param name="discount">The Discount with updated information.</param>
        Task UpdateAsync(Discount discount);

        /// <summary>
        /// Deletes a Discount by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Discount to delete.</param>
        Task DeleteAsync(string id);
    }
}