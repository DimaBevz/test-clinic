using Application.Patient.Commands;
using Application.Patient.DTOs.RequestDTOs;
using Application.Patient.DTOs.ResponseDTOs;
using Application.Patient.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class PatientController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetPatientDataDto>), 200)]
        public async Task<IActionResult> GetPatientData([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetPatientDataQuery(id));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<GetPatientDataDto>), 200)]
        public async Task<IActionResult> UpdatePatientData(UpdatePatientDto dto)
        {
            var result = await _mediator.Send(new UpdatePatientCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
