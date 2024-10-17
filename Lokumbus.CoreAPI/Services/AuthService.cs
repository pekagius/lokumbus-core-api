using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Helpers;
using Lokumbus.CoreAPI.Models;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the IAuthService interface for Auth business logic.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly TypeAdapterConfig _mapConfig;
        private readonly IProducer<Null, string> _kafkaProducer;
        private readonly string _kafkaTopic;

        /// <summary>
        /// Initializes a new instance of the AuthService class.
        /// </summary>
        /// <param name="authRepository">The Auth repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public AuthService(IAuthRepository authRepository, TypeAdapterConfig mapConfig, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _mapConfig = mapConfig;

            // Configure Kafka producer
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("KafkaSettings").GetValue<string>("BootstrapServers")
            };
            _kafkaProducer = new ProducerBuilder<Null, string>(producerConfig).Build();

            // Retrieve Kafka topic for ActivityService from configuration
            _kafkaTopic = configuration.GetSection("KafkaSettings").GetValue<string>("AuthTopic") 
                          ?? throw new ArgumentException("Kafka topic for ActivityService is not configured.");
        }

        /// <inheritdoc />
        public async Task<AuthDto?> GetByIdAsync(string id)
        {
            var auth = await _authRepository.GetByIdAsync(id);
            return auth?.Adapt<AuthDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthDto>> GetAllAsync()
        {
            var auths = await _authRepository.GetAllAsync();
            return auths.Adapt<IEnumerable<AuthDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AuthDto> CreateAsync(CreateAuthDto createAuthDto)
        {
            var auth = createAuthDto.Adapt<Auth>(_mapConfig);

            // Set creation timestamp and default values
            auth.CreatedAt = DateTime.UtcNow;
            auth.IsActive = true;

            // Insert the new Auth entry into the repository
            await _authRepository.CreateAsync(auth);

            // Publish creation event to Kafka
            var authDto = auth.Adapt<AuthDto>(_mapConfig);
            var message = authDto.ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });

            return authDto;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, UpdateAuthDto updateAuthDto)
        {
            var existingAuth = await _authRepository.GetByIdAsync(id);
            if (existingAuth == null)
            {
                throw new KeyNotFoundException($"Auth entry with ID {id} was not found.");
            }

            // Map update DTO to existing Auth entry
            updateAuthDto.Adapt(existingAuth, _mapConfig);
            existingAuth.UpdatedAt = DateTime.UtcNow;

            // Update the Auth entry in the repository
            await _authRepository.UpdateAsync(existingAuth);

            // Publish update event to Kafka
            var authDto = existingAuth.Adapt<AuthDto>(_mapConfig);
            var message = authDto.ToJson();
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingAuth = await _authRepository.GetByIdAsync(id);
            if (existingAuth == null)
            {
                throw new KeyNotFoundException($"Auth entry with ID {id} was not found.");
            }

            // Delete the Auth entry from the repository
            await _authRepository.DeleteAsync(id);

            // Publish deletion event to Kafka
            var message = $"Auth entry with ID {id} has been deleted.";
            await _kafkaProducer.ProduceAsync(_kafkaTopic, new Message<Null, string> { Value = message });
        }
    }
}