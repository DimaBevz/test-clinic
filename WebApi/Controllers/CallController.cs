using Application.Call.Commands;
using Application.Call.DTOs;
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

    [HttpPost]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> OnCallStarted(CallEventInfoDto callEventDto)
    {
        var result = await _mediator.Send(new StartCallCommand(callEventDto.Meeting));

        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> OnCallEnded(CallEventInfoDto callEventDto)
    {
        var result = await _mediator.Send(new EndCallCommand(callEventDto.Meeting));

        var response = result.ToApiResponse();
        return Ok(response);
    }
}
