using Application.Test.Commands;
using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;
using Application.Test.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

public class TestController : BaseApiController
{
    public TestController(IMediator mediator) : base(mediator){ }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<GetTestDto>>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTests(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTestsQuery(), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetQuestionsDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestQuestions(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTestQuestionsQuery(id), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(ApiResponse<TestProcessedDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProcessTestAnswers(
        [FromBody] TestAnswersDto testAnswersDto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ProcessTestAnswersCommand(testAnswersDto), cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(ApiResponse<List<GetAllTestResultsDto>>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTestResults([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTestResultsQuery(userId), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<CreatedTestDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestDto createTestDto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateTestCommand(createTestDto), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTest([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTestCommand(id), cancellationToken);
        
        var response = result.ToApiResponse();
        return Ok(response);
    }
}