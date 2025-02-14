using Lokumbus.CoreAPI.DTOs.Auth;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller für Social Logins (Google, Facebook, Apple).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SocialAuthController : ControllerBase
    {
        private readonly ISocialAuthService _socialAuthService;

        public SocialAuthController(ISocialAuthService socialAuthService)
        {
            _socialAuthService = socialAuthService;
        }

        /// <summary>
        /// POST /api/socialauth/google
        /// Body: { "provider":"google", "idToken":"...", "accessToken":"..." }
        /// </summary>
        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] SocialLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.IdToken))
                return BadRequest(new { Message = "Missing Google IdToken." });

            var userDto = await _socialAuthService.LoginWithGoogleAsync(dto.IdToken, dto.AccessToken);
            // Evtl. dein eigenes AccessToken generieren, z. B. GenerateJwtToken(userDto)
            return Ok(userDto);
        }

        /// <summary>
        /// POST /api/socialauth/facebook
        /// Body: { "provider":"facebook", "accessToken":"..." }
        /// </summary>
        [HttpPost("facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody] SocialLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.AccessToken))
                return BadRequest(new { Message = "Missing Facebook AccessToken." });

            var userDto = await _socialAuthService.LoginWithFacebookAsync(dto.AccessToken);
            return Ok(userDto);
        }

        /// <summary>
        /// POST /api/socialauth/apple
        /// Body: { "provider":"apple", "idToken":"...", "firstName":"...", "lastName":"...", "email":"..." }
        /// </summary>
        [HttpPost("apple")]
        public async Task<IActionResult> AppleLogin([FromBody] SocialLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.IdToken))
                return BadRequest(new { Message = "Missing Apple IdToken." });

            var userDto = await _socialAuthService.LoginWithAppleAsync(
                dto.IdToken, 
                dto.FirstName, 
                dto.LastName, 
                dto.Email);

            return Ok(userDto);
        }
    }
}