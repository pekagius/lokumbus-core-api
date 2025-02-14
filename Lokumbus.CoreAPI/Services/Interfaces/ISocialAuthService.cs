using Lokumbus.CoreAPI.DTOs;

namespace Lokumbus.CoreAPI.Services.Interfaces;

/// <summary>
/// Interface für Social-Logins (Google, Facebook, Apple).
/// </summary>
public interface ISocialAuthService
{
    /// <summary>
    /// Login/SignUp via Google.
    /// </summary>
    Task<AppUserDto> LoginWithGoogleAsync(string idToken, string? accessToken);

    /// <summary>
    /// Login/SignUp via Facebook.
    /// </summary>
    Task<AppUserDto> LoginWithFacebookAsync(string accessToken);

    /// <summary>
    /// Login/SignUp via Apple.
    /// </summary>
    Task<AppUserDto> LoginWithAppleAsync(string idToken, string? firstName, string? lastName, string? email);
}