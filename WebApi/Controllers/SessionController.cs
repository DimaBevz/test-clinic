using Application.Session.Commands;
using Application.Session.DTOs;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Application.Session.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class SessionController : BaseApiController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AddedSessionDto>), 200)]
        public async Task<IActionResult> AddSession(AddSessionDto requestDto)
        {
            var result = await _mediator.Send(new AddSessionCommand(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<GetSessionsResponseDto>), 200)]
        public async Task<IActionResult> GetSessions(GetSessionsRequestDto requestDto)
        {
            var result = await _mediator.Send(new GetSessionsQuery(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<GetPaginatedSessionsDto>), 200)]
        public async Task<IActionResult> GetSessionsByParams(GetSessionsByParamsDto requestDto)
        {
            var result = await _mediator.Send(new GetSessionsByParamsQuery(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet("{sessionId}")]
        [ProducesResponseType(typeof(ApiResponse<SessionDetailsDto>), 200)]
        public async Task<IActionResult> GetSessionById([FromRoute] Guid sessionId)
        {
            var result = await _mediator.Send(new GetSessionByIdQuery(sessionId));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<SessionDetailsDto>), 200)]
        public async Task<IActionResult> UpdateSession(UpdateSessionRequestDto requestDto)
        {
            var result = await _mediator.Send(new UpdateSessionCommand(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<SessionItemDto>), 200)]
        public async Task<IActionResult> UpdateArchiveStatus(Guid id)
        {
            var result = await _mediator.Send(new UpdateArchiveStatusCommand(id));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<SessionItemDto>), 200)]
        public async Task<IActionResult> DeleteSession(Guid sessionId)
        {
            var result = await _mediator.Send(new DeleteSessionCommand(sessionId));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
