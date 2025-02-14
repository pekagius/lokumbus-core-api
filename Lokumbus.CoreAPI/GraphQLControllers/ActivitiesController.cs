using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using NSubstitute.Exceptions;

namespace Lokumbus.CoreAPI.GraphQLControllers
{
    public class ActivitiesController : GraphController
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [QueryRoot("activities")]
        public async Task<IEnumerable<ActivityDto>> GetAll()
        {
            return await _activityService.GetAllAsync();
        }

        [QueryRoot("activity")]
        public async Task<ActivityDto> GetById(string id)
        {
            try
            {
                return await _activityService.GetByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe einer GraphQL-Fehlermeldung, wenn die Activity nicht existiert
                throw new ArgumentNotFoundException(ex.Message);
            }
        }

        [MutationRoot("addActivity")]
        public async Task<ActivityDto> Create(CreateActivityDto createDto)
        {
            return await _activityService.CreateAsync(createDto);
        }

        [MutationRoot("changeActivity")]
        public async Task<ActivityDto> Update(string id, UpdateActivityDto updateDto)
        {
            try
            {
                return await _activityService.UpdateAsync(id, updateDto);
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe einer GraphQL-Fehlermeldung, wenn die Activity nicht existiert
                throw new ArgumentNotFoundException(ex.Message);
            }
        }

        [MutationRoot("removeActivity")]
        public async Task<bool> Delete(string id)
        {
            try
            {
                await _activityService.DeleteAsync(id);
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                // Rückgabe einer GraphQL-Fehlermeldung, wenn die Activity nicht existiert
                throw new ArgumentNotFoundException(ex.Message);
            }
        }
    }
}