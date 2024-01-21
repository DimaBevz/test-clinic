using Application.Physician.Commands;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Application.Physician.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class PhysicianController : BaseApiController
    {
        public PhysicianController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetPhysicianDataDto>), 200)]
        public async Task<IActionResult> GetPhysicianData([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPhysicianDataQuery(id));
            
            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetPhysiciansDto>), 200)]
        public async Task<IActionResult> GetPhysicians()
        {
            var result = await _mediator.Send(new GetPhysiciansQuery());

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetPaginatedPhysiciansDto<PhysicianItemDto>>), 200)]
        public async Task<IActionResult> GetPhysiciansByParams([FromQuery] GetPhysiciansByParamsDto dto)
        {
            var result = await _mediator.Send(new GetPhysiciansByParamsQuery(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<GetPhysicianDataDto>), 200)]
        public async Task<IActionResult> UpdatePhysicianData(UpdatePhysicianDto dto)
        {
            var result = await _mediator.Send(new UpdatePhysicianCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
