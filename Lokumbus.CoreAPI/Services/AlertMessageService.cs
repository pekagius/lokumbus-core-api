using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Models.Enumerations;
using Lokumbus.CoreAPI.Models.SubClasses;
using Lokumbus.CoreAPI.Repositories.Interfaces;
using Lokumbus.CoreAPI.Services.Interfaces;
using Mapster;

namespace Lokumbus.CoreAPI.Services
{
    /// <summary>
    /// Implements the <see cref="IAlertMessageService"/> interface for AlertMessage business logic.
    /// </summary>
    public class AlertMessageService : IAlertMessageService
    {
        private readonly IAlertMessageRepository _alertMessageRepository;
        private readonly TypeAdapterConfig _mapConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertMessageService"/> class.
        /// </summary>
        /// <param name="alertMessageRepository">The AlertMessage repository instance.</param>
        /// <param name="mapConfig">The Mapster configuration.</param>
        public AlertMessageService(IAlertMessageRepository alertMessageRepository, TypeAdapterConfig mapConfig)
        {
            _alertMessageRepository = alertMessageRepository;
            _mapConfig = mapConfig;
        }

        /// <inheritdoc />
        public async Task<AlertMessageDto> GetByIdAsync(string id)
        {
            var alertMessage = await _alertMessageRepository.GetByIdAsync(id);
            if (alertMessage == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {id} was not found.");
            }

            return alertMessage.Adapt<AlertMessageDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AlertMessageDto>> GetAllAsync()
        {
            var alertMessages = await _alertMessageRepository.GetAllAsync();
            return alertMessages.Adapt<IEnumerable<AlertMessageDto>>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task<AlertMessageDto> CreateAsync(CreateAlertMessageDto createDto)
        {
            var alertMessage = createDto.Adapt<AlertMessage>(_mapConfig);
            alertMessage.CreatedAt = DateTime.UtcNow;
            alertMessage.Status = MessageStatus.Sent; // Beispielstatus

            await _alertMessageRepository.CreateAsync(alertMessage);
            return alertMessage.Adapt<AlertMessageDto>(_mapConfig);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UpdateAlertMessageDto updateDto)
        {
            var existingAlert = await _alertMessageRepository.GetByIdAsync(updateDto.Id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {updateDto.Id} was not found.");
            }

            updateDto.Adapt(existingAlert, _mapConfig);
            existingAlert.UpdatedAt = DateTime.UtcNow;

            await _alertMessageRepository.UpdateAsync(existingAlert);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            var existingAlert = await _alertMessageRepository.GetByIdAsync(id);
            if (existingAlert == null)
            {
                throw new KeyNotFoundException($"AlertMessage with ID {id} was not found.");
            }

            await _alertMessageRepository.DeleteAsync(id);
        }
    }
}