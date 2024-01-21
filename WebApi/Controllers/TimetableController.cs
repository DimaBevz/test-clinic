using Application.Patient.DTOs.ResponseDTOs;
using Application.TimetableTemplate.Commands;
using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;
using Application.TimetableTemplate.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class TimetableController : BaseApiController
    {
        public TimetableController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AddTimetableTemplateDto>), 200)]
        public async Task<IActionResult> CreateTimetableTemplate(AddTimetableTemplateDto requestDto)
        {
            var result = await _mediator.Send(new AddTimetableTemplateCommand(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<GetTimetablesDto>), 200)]
        public async Task<IActionResult> GetTimetableTemplate(GetTimetableTemplateDto requestDto)
        {
            var result = await _mediator.Send(new GetTimetableTemplateQuery(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet("{physicianId}")]
        [ProducesResponseType(typeof(ApiResponse<GetAvailableTimetableDto>), 200)]
        public async Task<IActionResult> GetAvailableTimetable([FromRoute] Guid physicianId)
        {
            var result = await _mediator.Send(new GetAvailableTimetableQuery(physicianId));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<UpdateTimetableTemplateDto>), 200)]
        public async Task<IActionResult> UpdateTimetableTemplate(UpdateTimetableTemplateDto requestDto)
        {
            var result = await _mediator.Send(new UpdateTimetableTemplateCommand(requestDto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> DeleteTimetableTemplate(Guid physicianId)
        {
            var result = await _mediator.Send(new DeleteTimetableTemplateCommand(physicianId));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
