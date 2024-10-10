using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories;

/// <summary>
/// Implements the IAppUserRepository interface for AppUser data access.
/// </summary>
public class AppUserRepository : IAppUserRepository
{
    private readonly IMongoCollection<AppUser> _appUsers;

    /// <summary>
    /// Initializes a new instance of the AppUserRepository class.
    /// </summary>
    /// <param name="database">The MongoDB database instance.</param>
    public AppUserRepository(IMongoDatabase database)
    {
        _appUsers = database.GetCollection<AppUser>("AppUsers");
    }

    /// <inheritdoc />
    public async Task<AppUser> GetByIdAsync(string id)
    {
        return await _appUsers.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _appUsers.Find(_ => true).ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(AppUser appUser)
    {
        await _appUsers.InsertOneAsync(appUser);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(AppUser appUser)
    {
        await _appUsers.ReplaceOneAsync(user => user.Id == appUser.Id, appUser);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string id)
    {
        await _appUsers.DeleteOneAsync(user => user.Id == id);
    }
}