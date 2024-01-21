using Application.Chat.DTOs.ResponseDTOs;
using Application.Chat.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class ChatController : BaseApiController
    {
        public ChatController(IMediator mediator) : base(mediator) {}

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetChatMessagesDto>), 200)]
        public async Task<IActionResult> GetSessionMessages([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetSessionMessagesQuery(id));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
