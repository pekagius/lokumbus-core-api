using System.Net.Http.Headers;
using System.Text.Json;
using Confluent.Kafka;
using Google.Apis.Auth; 
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;


namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Konkrete Implementierung für Google/Facebook/Apple-Logins.
    /// Holt alle möglichen Felder (Name, E-Mail, Bild, etc.) via Provider-APIs.
    /// </summary>
    public class SocialAuthService : ISocialAuthService, IDisposable
    {
        private readonly IAppUserRepository _appUserRepo;
        private readonly HttpClient _httpClient;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IConfiguration _config;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private bool _disposed;

        public SocialAuthService(
            IAppUserRepository appUserRepo,
            TypeAdapterConfig mapConfig,
            IConfiguration config)
        {
            _appUserRepo = appUserRepo;
            _mapConfig = mapConfig;
            _config = config;
            _httpClient = new HttpClient();

            // Kafka (optional, weil du viel in Code hast)
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = _config["KafkaSettings:BootstrapServers"]
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
            _kafkaTopic = _config["KafkaSettings:UserEventsTopic"] ?? "user-events";
        }

        /// <summary>
        /// Google Login: ID-Token validieren + optional AccessToken -> Userinfo-Endpoint.
        /// </summary>
        public async Task<AppUserDto> LoginWithGoogleAsync(string idToken, string? accessToken)
        {
            // 1) ID-Token verifizieren
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _config["GoogleOAuth:ClientId"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            // => Da hast du Email, Subject (GoogleUserID), Name, Picture ...
            
            // 2) Falls AccessToken vorhanden, können wir /userinfo aufrufen
            string? firstName = null;
            string? lastName = null;
            string? pictureUrl = null;
            string? gender = null; // Google liefert das i.d.R. nicht
            DateTime? birthday = null; // Google liefert es i.d.R. nicht ohne scope "profile" plus "birthdays"

            if (!string.IsNullOrEmpty(accessToken))
            {
                // Rufe userinfo-Endpoint auf
                var req = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v3/userinfo");
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var resp = await _httpClient.SendAsync(req);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                
                // Felder z. B. "email", "given_name", "family_name", "name", "picture", "birthday"...
                firstName = doc.RootElement.TryGetProperty("given_name", out var fEl) ? fEl.GetString() : null;
                lastName = doc.RootElement.TryGetProperty("family_name", out var lEl) ? lEl.GetString() : null;
                pictureUrl = doc.RootElement.TryGetProperty("picture", out var pEl) ? pEl.GetString() : null;
            }

            // 3) DB check
            //    Email => payload.Email
            //    GoogleUserId => payload.Subject
            //    Name => payload.Name
            var user = await FindOrCreateUser(
                provider: "google",
                providerUserId: payload.Subject,
                email: payload.Email,
                fallbackName: payload.Name,
                firstName: firstName,
                lastName: lastName,
                avatarUrl: pictureUrl,
                birthday: birthday,
                gender: gender
            );

            await PublishKafkaEventAsync($"[GoogleLogin] User {user.Id} logged in.");
            return user;
        }

        /// <summary>
        /// Facebook Login: AccessToken debuggen + /me?fields=...
        /// </summary>
        public async Task<AppUserDto> LoginWithFacebookAsync(string accessToken)
        {
            var fbAppId = _config["FacebookOAuth:AppId"];
            var fbAppSecret = _config["FacebookOAuth:AppSecret"];

            // 1) Token debug
            var debugUrl = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={fbAppId}|{fbAppSecret}";
            var debugResp = await _httpClient.GetAsync(debugUrl);
            debugResp.EnsureSuccessStatusCode();
            var debugBody = await debugResp.Content.ReadAsStringAsync();
            using var debugDoc = JsonDocument.Parse(debugBody);
            var isValid = debugDoc.RootElement
                .GetProperty("data")
                .GetProperty("is_valid")
                .GetBoolean();
            if (!isValid) throw new Exception("Facebook access token invalid or expired.");

            var fbUserId = debugDoc.RootElement
                .GetProperty("data")
                .GetProperty("user_id")
                .GetString();

            // 2) /me?fields=...
            //   Hier E-Mail, FirstName, LastName, Birthday, Gender, Picture
            var meUrl = $"https://graph.facebook.com/me?fields=id,name,email,first_name,last_name,birthday,gender,picture.width(512).height(512)&access_token={accessToken}";
            var meResp = await _httpClient.GetAsync(meUrl);
            meResp.EnsureSuccessStatusCode();
            var meBody = await meResp.Content.ReadAsStringAsync();
            using var meDoc = JsonDocument.Parse(meBody);

            var email = meDoc.RootElement.TryGetProperty("email", out var emailEl) ? emailEl.GetString() : null;
            var firstName = meDoc.RootElement.TryGetProperty("first_name", out var fEl) ? fEl.GetString() : null;
            var lastName = meDoc.RootElement.TryGetProperty("last_name", out var lEl) ? lEl.GetString() : null;
            var name = meDoc.RootElement.TryGetProperty("name", out var nEl) ? nEl.GetString() : null;
            var birthdayStr = meDoc.RootElement.TryGetProperty("birthday", out var bEl) ? bEl.GetString() : null;
            var gender = meDoc.RootElement.TryGetProperty("gender", out var gEl) ? gEl.GetString() : null;

            DateTime? birthday = null;
            if (!string.IsNullOrEmpty(birthdayStr))
            {
                // Facebook-Birthday typically "MM/DD/YYYY"
                if (DateTime.TryParse(birthdayStr, out var dt))
                    birthday = dt;
            }

            string? pictureUrl = null;
            if (meDoc.RootElement.TryGetProperty("picture", out var picEl) &&
                picEl.TryGetProperty("data", out var dataEl) &&
                dataEl.TryGetProperty("url", out var urlEl))
            {
                pictureUrl = urlEl.GetString();
            }

            var user = await FindOrCreateUser(
                provider: "facebook",
                providerUserId: fbUserId,
                email: email,
                fallbackName: name,
                firstName: firstName,
                lastName: lastName,
                avatarUrl: pictureUrl,
                birthday: birthday,
                gender: gender
            );

            await PublishKafkaEventAsync($"[FacebookLogin] User {user.Id} logged in.");
            return user;
        }

        /// <summary>
        /// Apple Login. Apple ID Token -> minimal verifizieren + 
        /// FirstName, LastName, Email optional vom Client empfangen, da Apple es nur 1x liefert.
        /// </summary>
        public async Task<AppUserDto> LoginWithAppleAsync(string idToken, string? firstName, string? lastName, string? email)
        {
            // 1) Apple JWT check
            //    In echt: Public keys von "https://appleid.apple.com/auth/keys" abrufen,
            //    Signatur + Aud claim prüfen, etc.
            //    Hier Pseudo:
            var appleSub = "someAppleUserIdFromToken";
            var tokenValid = true; 
            if (!tokenValid) throw new Exception("Apple token invalid.");

            // 2) E-Mail nur beim ersten Mal. Falls wir hier keine haben, user hat sie evtl. schon "verloren".
            // fallback auf param email, wenn nicht da => null => wir generieren notfalls was
            var user = await FindOrCreateUser(
                provider: "apple",
                providerUserId: appleSub,
                email: email,
                fallbackName: null,
                firstName: firstName,
                lastName: lastName,
                avatarUrl: null,
                birthday: null,
                gender: null
            );

            await PublishKafkaEventAsync($"[AppleLogin] User {user.Id} logged in.");
            return user;
        }

        /// <summary>
        /// Sucht User anhand E-Mail oder generiert ihn. 
        /// Speichert optional Name, Birthday, Gender, Avatar etc.
        /// </summary>
        private async Task<AppUserDto> FindOrCreateUser(
            string provider,
            string providerUserId,
            string? email,
            string? fallbackName,
            string? firstName,
            string? lastName,
            string? avatarUrl,
            DateTime? birthday,
            string? gender)
        {
            // 1) Falls wir E-Mail haben, checken wir, ob dieser User existiert
            AppUser? user = null;

            if (!string.IsNullOrEmpty(email))
            {
                user = await _appUserRepo.GetByEmailAsync(email);
            }

            if (user == null)
            {
                // Erstelle neuen
                user = new AppUser
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Email = email,
                    Username = !string.IsNullOrEmpty(firstName) 
                               ? firstName + " " + lastName 
                               : fallbackName ?? (provider + "_" + providerUserId),
                    FirstName = firstName,
                    LastName = lastName,
                    AvatarUrl = avatarUrl,
                    DateOfBirth = birthday,
                    Gender = gender,
                    IsActive = true,
                    IsVerified = true, // bei Social Login i. d. R. verified
                    CreatedAt = DateTime.UtcNow,
                    LastLoginAt = DateTime.UtcNow
                };
                await _appUserRepo.CreateAsync(user);
            }
            else
            {
                // Update Felder, wenn leer
                user.LastLoginAt = DateTime.UtcNow;
                if (string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(firstName))
                    user.FirstName = firstName;
                if (string.IsNullOrEmpty(user.LastName) && !string.IsNullOrEmpty(lastName))
                    user.LastName = lastName;
                if (string.IsNullOrEmpty(user.AvatarUrl) && !string.IsNullOrEmpty(avatarUrl))
                    user.AvatarUrl = avatarUrl;
                if (!user.DateOfBirth.HasValue && birthday.HasValue)
                    user.DateOfBirth = birthday;
                if (string.IsNullOrEmpty(user.Gender) && !string.IsNullOrEmpty(gender))
                    user.Gender = gender;

                await _appUserRepo.UpdateAsync(user);
            }

            // => Return mapped
            return user.Adapt<AppUserDto>(_mapConfig);
        }

        /// <summary>
        /// Einfaches Kafka-Event publizieren.
        /// </summary>
        private async Task PublishKafkaEventAsync(string message)
        {
            var msg = new Message<Null, string> { Value = message };
            await _kafkaProducer.ProduceAsync(_kafkaTopic, msg);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _httpClient.Dispose();
                _kafkaProducer.Dispose();
                _disposed = true;
            }
        }
    }
}