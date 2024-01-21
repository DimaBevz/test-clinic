using Application.Comment.Commands;
using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;
using Application.Comment.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

public class CommentController : BaseApiController
{
    public CommentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateCommentResponseDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateComment(
        [FromBody] CreateCommentRequestDto createCommentRequestDto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateCommentCommand(createCommentRequestDto), cancellationToken);
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<GetCommentResponseDto>>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetComments(Guid physicianId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCommentsQuery(physicianId), cancellationToken);
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpDelete]
    [Authorize(Roles = "Patient,Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteComment(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCommentCommand(id), cancellationToken);
        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpPut]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(typeof(ApiResponse<UpdateCommentResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateComment(
        [FromBody] UpdateCommentRequestDto updateCommentRequestDto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateCommentCommand(updateCommentRequestDto), cancellationToken);
        var response = result.ToApiResponse();
        return Ok(response);
    }
    
}