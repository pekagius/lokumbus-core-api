using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing Auth entries.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="authService">The Auth service instance.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Retrieves all Auth entries.
        /// </summary>
        /// <returns>A collection of AuthDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthDto>>> GetAll()
        {
            var auths = await _authService.GetAllAsync();
            return Ok(auths);
        }

        /// <summary>
        /// Retrieves a specific Auth entry by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry.</param>
        /// <returns>The requested AuthDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthDto>> GetById(string id)
        {
            var auth = await _authService.GetByIdAsync(id);
            if (auth == null)
            {
                return NotFound(new { Message = $"Auth entry with ID {id} was not found." });
            }
            return Ok(auth);
        }

        /// <summary>
        /// Creates a new Auth entry.
        /// </summary>
        /// <param name="createAuthDto">The DTO containing creation data.</param>
        /// <returns>The created AuthDto.</returns>
        [HttpPost]
        public async Task<ActionResult<AuthDto>> Create([FromBody] CreateAuthDto createAuthDto)
        {
            var createdAuth = await _authService.CreateAsync(createAuthDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAuth.Id }, createdAuth);
        }

        /// <summary>
        /// Updates an existing Auth entry.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry to update.</param>
        /// <param name="updateAuthDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAuthDto updateAuthDto)
        {
            try
            {
                await _authService.UpdateAsync(id, updateAuthDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an Auth entry by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Auth entry to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _authService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}