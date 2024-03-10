using Application.BugReport.Commands;
using Application.BugReport.DTOs.ResponseDTOs;
using Application.BugReport.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BugReport;
using WebApi.Extensions;
using WebApi.Mappers;

namespace WebApi.Controllers;

public class BugReportController : BaseApiController
{
    public BugReportController(IMediator mediator) : base(mediator) {}
    
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SentBugReportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendBugReport([FromForm] SendBugReportFormDto sendBugReportFormDto, CancellationToken cancellationToken)
    {
        var sendBugReportDto = sendBugReportFormDto.ToSendBugReportDto();
        var result = await _mediator.Send(new SendBugReportCommand(sendBugReportDto), cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBugReports(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBugReportsQuery(), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }
}