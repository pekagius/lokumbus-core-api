// ChatMessagesController.cs
using Lokumbus.CoreAPI.DTOs;
using Lokumbus.CoreAPI.DTOs.Create;
using Lokumbus.CoreAPI.DTOs.Update;
using Lokumbus.CoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lokumbus.CoreAPI.Controllers
{
    /// <summary>
    /// Controller for managing ChatMessage resources.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessagesController"/> class.
        /// </summary>
        /// <param name="chatMessageService">The ChatMessage service instance.</param>
        public ChatMessagesController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        /// <summary>
        /// Retrieves all ChatMessages.
        /// </summary>
        /// <returns>A collection of ChatMessageDto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetAll()
        {
            var chatMessages = await _chatMessageService.GetAllAsync();
            return Ok(chatMessages);
        }

        /// <summary>
        /// Retrieves a specific ChatMessage by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage.</param>
        /// <returns>The requested ChatMessageDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessageDto>> GetById(string id)
        {
            try
            {
                var chatMessage = await _chatMessageService.GetByIdAsync(id);
                return Ok(chatMessage);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new ChatMessage.
        /// </summary>
        /// <param name="createDto">The DTO containing creation data.</param>
        /// <returns>The created ChatMessageDto.</returns>
        [HttpPost]
        public async Task<ActionResult<ChatMessageDto>> Create([FromBody] CreateChatMessageDto createDto)
        {
            var createdChatMessage = await _chatMessageService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdChatMessage.Id }, createdChatMessage);
        }

        /// <summary>
        /// Updates an existing ChatMessage.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage to update.</param>
        /// <param name="updateDto">The DTO containing update data.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateChatMessageDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            try
            {
                await _chatMessageService.UpdateAsync(updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a ChatMessage by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the ChatMessage to delete.</param>
        /// <returns>An IActionResult indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _chatMessageService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}