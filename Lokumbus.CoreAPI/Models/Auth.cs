using Lokumbus.CoreAPI.Models.Log;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lokumbus.CoreAPI.Models
{
    public class Auth
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string? Provider { get; set; }
        public string? ProviderUserId { get; set; }
        public string? ProviderAccessToken { get; set; }
        public string? ProviderRefreshToken { get; set; }
        public DateTime? ProviderAccessTokenExpires { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }
        public string? DeviceModel { get; set; }
        public string? DeviceOperatingSystem { get; set; }
        public string? DeviceOperatingSystemVersion { get; set; }
        public string? Location { get; set; }
        public List<AuthenticationLog>? AuthenticationLogs { get; set; }
        public List<AuthorizationLog>? AuthorizationLogs { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}