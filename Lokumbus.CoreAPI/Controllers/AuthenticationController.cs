using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lokumbus.CoreAPI.DTOs.Auth;
using Lokumbus.CoreAPI.Helpers;

namespace Lokumbus.CoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAppUserService appUserService, IConfiguration configuration)
        {
            _appUserService = appUserService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authentifiziert einen Benutzer und gibt Tokens zurück.
        /// </summary>
        /// <param name="loginDto">Die Anmeldedaten.</param>
        /// <returns>Access-Token und Refresh-Token.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _appUserService.GetByEmailAsync(loginDto.Email);
            if (user == null || !PasswordHelper.VerifyPassword(user.PasswordHash, loginDto.Password))
            {
                return Unauthorized(new { Message = "Ungültige Anmeldedaten." });
            }

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Speichern des Refresh-Tokens in der Datenbank
            await _appUserService.SetRefreshTokenAsync(user.Id, refreshToken);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Aktualisiert das Access-Token mithilfe eines Refresh-Tokens.
        /// </summary>
        /// <param name="tokenDto">Das Refresh-Token.</param>
        /// <returns>Neues Access-Token und Refresh-Token.</returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            if (principal == null)
            {
                return BadRequest(new { Message = "Ungültiges Access-Token." });
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var isValid = await _appUserService.ValidateRefreshTokenAsync(userId, tokenDto.RefreshToken);
            if (!isValid)
            {
                return BadRequest(new { Message = "Ungültiges oder abgelaufenes Refresh-Token." });
            }

            var user = await _appUserService.GetByIdAsync(userId);
            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();
            await _appUserService.SetRefreshTokenAsync(user.Id, newRefreshToken);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Generiert ein JWT-Access-Token.
        /// </summary>
        /// <param name="user">Der Benutzer.</param>
        /// <returns>Das JWT-Token.</returns>
        private string GenerateJwtToken(AppUserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expirationMinutes = jwtSettings.GetValue<int>("AccessTokenExpirationMinutes");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.Username ?? "")
                // Weitere Claims hinzufügen, falls benötigt
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generiert ein Refresh-Token.
        /// </summary>
        /// <returns>Das Refresh-Token.</returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Extrahiert die ClaimsPrincipal aus einem abgelaufenen Token.
        /// </summary>
        /// <param name="token">Das abgelaufene Token.</param>
        /// <returns>Der ClaimsPrincipal oder null.</returns>
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false // Wir validieren die Lebensdauer nicht hier
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}