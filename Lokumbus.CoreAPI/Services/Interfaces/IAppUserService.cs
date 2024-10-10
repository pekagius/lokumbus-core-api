using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;

namespace Lokumbus.CoreAPI.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for AppUser service operations.
    /// </summary>
    public interface IAppUserService
    {
        Task<AppUserDto> GetByIdAsync(string id);
        Task<AppUserDto> GetByEmailAsync(string email); // Neue Methode
        Task<IEnumerable<AppUserDto>> GetAllAsync();
        Task<AppUserDto> CreateAsync(CreateAppUserDto createDto);
        Task UpdateAsync(string id, UpdateAppUserDto updateDto);
        Task DeleteAsync(string id);

        // Methoden zur Verwaltung der Refresh-Tokens
        Task SetRefreshTokenAsync(string userId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
    }
}