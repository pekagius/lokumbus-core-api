using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace Lokumbus.CoreAPI.Repositories
{
    /// <summary>
    /// Implements the IAppUserRepository interface for AppUser data access.
    /// </summary>
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IMongoCollection<AppUser> _appUsers;

        public AppUserRepository(IMongoDatabase database)
        {
            _appUsers = database.GetCollection<AppUser>("AppUsers");
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _appUsers.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await _appUsers.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _appUsers.Find(_ => true).ToListAsync();
        }

        public async Task CreateAsync(AppUser appUser)
        {
            await _appUsers.InsertOneAsync(appUser);
        }

        public async Task UpdateAsync(AppUser appUser)
        {
            await _appUsers.ReplaceOneAsync(user => user.Id == appUser.Id, appUser);
        }

        public async Task DeleteAsync(string id)
        {
            await _appUsers.DeleteOneAsync(user => user.Id == id);
        }
    }
}