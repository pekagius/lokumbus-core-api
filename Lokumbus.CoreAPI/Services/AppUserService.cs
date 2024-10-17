using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Confluent.Kafka;
using Mapster;


namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the IAppUserService interface for AppUser business logic.
    /// </summary>
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the AppUserService class.
        /// </summary>
        /// <param name="appUserRepository">The AppUser repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public AppUserService(IAppUserRepository appUserRepository, TypeAdapterConfig mapConfig,
            IConfiguration configuration)
        {
            _appUserRepository = appUserRepository;
            _mapConfig = mapConfig;
            _configuration = configuration;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for ActivityService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("AppUserTopic") 
                          ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
        }

        /// <inheritdoc />
        public async Task<AppUserDto> GetByIdAsync(string id)
        {
            var appUser = await _appUserRepository.GetByIdAsync(id);
            if (appUser == null)
            {
                throw new KeyNotFoundException($"AppUser mit ID {id} wurde nicht gefunden.");
            }

            return appUser.Adapt<AppUserDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AppUserDto> GetByEmailAsync(string email)
        {
            var appUser = await _appUserRepository.GetByEmailAsync(email);
            if (appUser == null)
            {
                throw new KeyNotFoundException($"AppUser mit Email {email} wurde nicht gefunden.");
            }

            return appUser.Adapt<AppUserDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AppUserDto>> GetAllAsync()
        {
            var appUsers = await _appUserRepository.GetAllAsync();
            return appUsers.Adapt<IEnumerable<AppUserDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AppUserDto> CreateAsync(CreateAppUserDto createDto)
        {
            var hashedPassword = PasswordHelper.HashPassword(createDto.Password!);

            var appUser = createDto.Adapt<AppUser>(_mapConfig);
            appUser.PasswordHash = hashedPassword;

            appUser.CreatedAt = DateTime.UtcNow;
            appUser.IsActive = true;
            appUser.IsVerified = false;

            await _appUserRepository.CreateAsync(appUser);

            var message = appUser.Adapt<AppUserDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return appUser.Adapt<AppUserDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateAppUserDto updateDto)
        {
            var existingUser = await _appUserRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"AppUser mit ID {id} wurde nicht gefunden.");
            }

            updateDto.Adapt(existingUser, _mapConfig);
            existingUser.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(updateDto.Password))
            {
                existingUser.PasswordHash = PasswordHelper.HashPassword(updateDto.Password);
            }

            if (!string.IsNullOrEmpty(existingUser.RefreshToken))
            {
                existingUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                    _configuration.GetSection("JwtSettings").GetValue<int>("RefreshTokenExpirationDays")
                );
            }

            await _appUserRepository.UpdateAsync(existingUser);
            var message = existingUser.Adapt<AppUserDto>(_mapConfig).ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingUser = await _appUserRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"AppUser mit ID {id} wurde nicht gefunden.");
            }

            await _appUserRepository.DeleteAsync(id);

            var message = $"AppUser mit ID {id} wurde gelöscht.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task SetRefreshTokenAsync(string userId, string refreshToken)
        {
            var user = await _appUserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"AppUser mit ID {userId} wurde nicht gefunden.");
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                _configuration.GetSection("JwtSettings").GetValue<int>("RefreshTokenExpirationDays")
            );

            await _appUserRepository.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            var user = await _appUserRepository.GetByIdAsync(userId);
            return user.RefreshToken == refreshToken && !(user.RefreshTokenExpiryTime <= DateTime.UtcNow);
        }
    }
}