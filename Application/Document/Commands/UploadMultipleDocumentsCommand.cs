using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Document.DTOs;
using Application.Document.DTOs.RequestDTOs;
using Application.Document.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Document.Commands;

public record UploadMultipleDocumentsCommand(
    UploadMultipleDocumentsDto UploadMultipleDocumentsDto) : ICommand<List<DocumentUploadedDto>>;

public sealed class UploadMultipleDocumentsCommandHandler : ICommandHandler<UploadMultipleDocumentsCommand, List<DocumentUploadedDto>>
{
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDocumentRepository _documentRepository;
    private const string ExceptionMessage = "Document upload aborted";
    
    public UploadMultipleDocumentsCommandHandler(
        IFileService fileService, 
        ICurrentUserService currentUserService, 
        IDocumentRepository documentRepository)
    {
        _fileService = fileService;
        _currentUserService = currentUserService;
        _documentRepository = documentRepository;
    }
    
    public async ValueTask<List<DocumentUploadedDto>> Handle(UploadMultipleDocumentsCommand command, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId);

        var awsUploadResponses = await Task.WhenAll(command.UploadMultipleDocumentsDto.Streams.Select(
            async (x, i) =>
            {
                var fileName = Guid.NewGuid().ToString();
                return await _fileService
                    .UploadFileAsync(
                        x,
                        command.UploadMultipleDocumentsDto.ContentTypes[i],
                        FolderName.Documents,
                        fileName);
            }
            )
        );
                        
        
        if (awsUploadResponses.Any(x=>x is null))
        {
            throw new ApplicationException(ExceptionMessage);
        }
        
        var dbResponse = await _documentRepository
            .AddMultipleDocumentsInfoAsync(
                command.UploadMultipleDocumentsDto.FileNames, 
                command.UploadMultipleDocumentsDto.DocumentTypes, 
                userId, 
                awsUploadResponses, 
                command.UploadMultipleDocumentsDto.SessionId, 
                cancellationToken);
        
        if (dbResponse is null || dbResponse.Any(x=> x is null))
        {
            throw new ApplicationException(ExceptionMessage);
        }

        foreach (var stream in command.UploadMultipleDocumentsDto.Streams)
        {
            await stream.DisposeAsync();
        }
        
        return dbResponse;
    }
}