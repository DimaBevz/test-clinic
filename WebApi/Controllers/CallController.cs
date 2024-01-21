using Application.Call.Commands;
using Application.Chat.DTOs.ResponseDTOs;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

public class CallController : BaseApiController
{
    public CallController(IMediator mediator) : base(mediator) {}

    [HttpGet("{sessionId:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetChatMessagesDto>), 200)]
    public async Task<IActionResult> GetSessionCallToken([FromRoute] Guid sessionId)
    {
        var result = await _mediator.Send(new CreateCallCommand(sessionId));

        var response = result.ToApiResponse();
        return Ok(response);
    }
}
