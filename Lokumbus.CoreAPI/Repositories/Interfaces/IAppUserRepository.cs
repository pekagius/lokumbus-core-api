using Lokumbus.CoreAPI.Models;

namespace Lokumbus.CoreAPI.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for AppUser repository operations.
    /// </summary>
    public interface IAppUserRepository
    {
        Task<AppUser> GetByIdAsync(string id);
        Task<AppUser> GetByEmailAsync(string email); 
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task CreateAsync(AppUser appUser);
        Task UpdateAsync(AppUser appUser);
        Task DeleteAsync(string id);
    }
}