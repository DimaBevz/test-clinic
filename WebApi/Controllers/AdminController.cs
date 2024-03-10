using Application.Admin.Commands;
using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.Admin.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class AdminController : BaseApiController
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetAllUsersDto>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(ApiResponse<GetUserItemDto>), 200)]
        public async Task<IActionResult> ChangeBanStatus([FromRoute] Guid userId)
        {
            var result = await _mediator.Send(new ChangeBanStatusCommand(userId));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetUnapprovedPhysiciansDto>), 200)]
        public async Task<IActionResult> GetUnapprovedPhysicians()
        {
            var result = await _mediator.Send(new GetUnapprovedPhysiciansQuery());

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<ApprovedPhysicianDto>), 200)]
        public async Task<IActionResult> ApprovePhysician(ApprovePhysicianDto physicianDto)
        {
            var result = await _mediator.Send(new ApprovePhysicianCommand(physicianDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<DeclinedPhysicianDto>), 200)]
        public async Task<IActionResult> DeclinePhysician(DeclinePhysicianDto physicianDto)
        {
            var result = await _mediator.Send(new DeclinePhysicianCommand(physicianDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
