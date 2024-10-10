using MongoDB.Driver;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implements the ICategoryRepository interface for Category data access.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;

    /// <summary>
    /// Initializes a new instance of the CategoryRepository class.
    /// </summary>
    /// <param name="database">The MongoDB database instance.</param>
    public CategoryRepository(IMongoDatabase database)
    {
        _categories = database.GetCollection<Category>("Categories");
    }

    /// <inheritdoc />
    public async Task<Category> GetByIdAsync(string id)
    {
        return await _categories.Find(category => category.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetAllActiveAsync()
    {
        return await _categories.Find(category => category.IsActive == true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categories.Find(_ => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Category category)
    {
        await _categories.InsertOneAsync(category);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Category category)
    {
        await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _categories.DeleteOneAsync(category => category.Id == id);
    }
}