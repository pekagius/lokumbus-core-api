using System;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.ValueObjects;

namespace Lokumbus.CoreAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing Auth details.
    /// </summary>
    public class AuthDto
    {
        /// <summary>
        /// The unique identifier of the Auth entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique identifier of the associated AppUser.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The authentication provider (e.g., Google, Facebook).
        /// </summary>
        public string? Provider { get; set; }

        /// <summary>
        /// The user identifier provided by the authentication provider.
        /// </summary>
        public string? ProviderUserId { get; set; }

        /// <summary>
        /// The access token provided by the authentication provider.
        /// </summary>
        public string? ProviderAccessToken { get; set; }

        /// <summary>
        /// The refresh token provided by the authentication provider.
        /// </summary>
        public string? ProviderRefreshToken { get; set; }

        /// <summary>
        /// The expiration time of the provider's access token.
        /// </summary>
        public DateTime? ProviderAccessTokenExpires { get; set; }

        /// <summary>
        /// The IP address from which the authentication occurred.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// The user agent string of the client's browser or device.
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// The device identifier.
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// The type of the device (e.g., Mobile, Desktop).
        /// </summary>
        public string? DeviceType { get; set; }

        /// <summary>
        /// The model of the device.
        /// </summary>
        public string? DeviceModel { get; set; }

        /// <summary>
        /// The operating system of the device.
        /// </summary>
        public string? DeviceOperatingSystem { get; set; }

        /// <summary>
        /// The version of the device's operating system.
        /// </summary>
        public string? DeviceOperatingSystemVersion { get; set; }

        /// <summary>
        /// The location associated with the authentication event.
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Indicates whether the Auth entry is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// The date and time when the Auth entry was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the Auth entry was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Metadata associated with the Auth entry.
        /// </summary>
        public List<MetaEntry>? Metadata { get; set; }
    }
}