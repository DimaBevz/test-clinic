using Application.Position.Commands;
using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;
using Application.Position.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class PositionController : BaseApiController
    {
        public PositionController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetPositionsDto>), 200)]
        public async Task<IActionResult> GetPositions()
        {
            var result = await _mediator.Send(new GetPositionsQuery());

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<GetPositionsDto>), 200)]
        public async Task<IActionResult> AddPositions(AddPositionListDto dto)
        {
            var result = await _mediator.Send(new AddPositionRangeCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PositionDto>), 200)]
        public async Task<IActionResult> UpdatePosition(UpdatePositionDto dto)
        {
            var result = await _mediator.Send(new UpdatePositionCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<PositionDto>), 200)]
        public async Task<IActionResult> RemovePosition([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeletePositionCommand(id));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
