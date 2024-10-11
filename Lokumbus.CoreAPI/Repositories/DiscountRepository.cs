using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IDiscountRepository interface for Discount data access.
    /// </summary>
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IMongoCollection<Discount> _discounts;

        /// <summary>
        /// Initializes a new instance of the DiscountRepository class.
        /// </summary>
        /// <param name="database">The MongoDB database instance.</param>
        public DiscountRepository(IMongoDatabase database)
        {
            _discounts = database.GetCollection<Discount>("Discounts");
        }

        /// <inheritdoc />
        public async Task<Discount?> GetByIdAsync(string id)
        {
            return await _discounts.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _discounts.Find(_ => true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateAsync(Discount discount)
        {
            await _discounts.InsertOneAsync(discount);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Discount discount)
        {
            await _discounts.ReplaceOneAsync(d => d.Id == discount.Id, discount);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            await _discounts.DeleteOneAsync(d => d.Id == id);
        }
    }
}