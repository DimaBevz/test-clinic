using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Document.DTOs;
using Application.Document.DTOs.RequestDTOs;
using Application.Document.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Document.Commands;

public record UploadDocumentCommand(UploadDocumentDto UploadDocumentDto) : ICommand<DocumentUploadedDto>;

public sealed class UploadDocumentCommandHandler : ICommandHandler<UploadDocumentCommand, DocumentUploadedDto>
{
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDocumentRepository _documentRepository;
    private const string ExceptionMessage = "Document upload aborted";
    
    public UploadDocumentCommandHandler(
        IFileService fileService, 
        ICurrentUserService currentUserService, 
        IDocumentRepository documentRepository)
    {
        _fileService = fileService;
        _currentUserService = currentUserService;
        _documentRepository = documentRepository;
    }
    
    public async ValueTask<DocumentUploadedDto> Handle(UploadDocumentCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId);
        var fileAwsId = Guid.NewGuid().ToString();
        
        var awsUploadResponse = await _fileService
            .UploadFileAsync(
                command.UploadDocumentDto.FileStream!, 
                command.UploadDocumentDto.ContentType!, 
                FolderName.Documents, 
                fileAwsId);
        if (awsUploadResponse is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }
        
        var dbResponse = await _documentRepository
            .AddDocumentInfoAsync(
                command.UploadDocumentDto.FileName!, 
                command.UploadDocumentDto.DocumentType, 
                userId, 
                awsUploadResponse,
                command.UploadDocumentDto.SessionId,
                cancellationToken);
        if (dbResponse is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }
        await command.UploadDocumentDto.FileStream!.DisposeAsync();
        return dbResponse;
    }
}