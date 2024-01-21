using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Document.Commands;
using Application.Document.DTOs;
using Application.Document.DTOs.RequestDTOs;
using Application.Document.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Document;
using WebApi.Extensions;
using WebApi.Mappers;

namespace WebApi.Controllers;

public class DocumentController : BaseApiController
{
    public DocumentController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<FileDataDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadDocument(
        [FromForm] UploadDocumentFormDto uploadDocumentFormDto,
        CancellationToken cancellationToken)
    {
        var uploadDocumentDto = uploadDocumentFormDto.ToUploadDocumentDto();
        var result = await _mediator.Send(
            new UploadDocumentCommand( 
                uploadDocumentDto),
            cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<FileDataDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDocuments(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDocumentsQuery(userId), cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDocument(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteDocumentCommand(id), cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<List<FileDataDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadMultipleDocuments(
        [FromForm] UploadMultipleDocumentsFormDto uploadMultipleDocumentsFormDto,
        CancellationToken cancellationToken)
    {
        var uploadMultipleDocumentsDto = uploadMultipleDocumentsFormDto.ToUploadMultipleDocumentsDto();
        var result = await _mediator.Send(
            new UploadMultipleDocumentsCommand(uploadMultipleDocumentsDto), 
            cancellationToken);

        var response = result.ToApiResponse();
        return Ok(response);
    }
}