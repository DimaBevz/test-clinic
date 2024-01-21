using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Document.Commands;

public record DeleteDocumentCommand(Guid Id) : ICommand<bool>;

public sealed class DeleteDocumentCommandHandler : ICommandHandler<DeleteDocumentCommand, bool>
{
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDocumentRepository _documentRepository;
    private const string ExceptionMessage = "Document deletion aborted";
    
    public DeleteDocumentCommandHandler(
        IFileService fileService, 
        ICurrentUserService currentUserService, 
        IDocumentRepository documentRepository)
    {
        _fileService = fileService;
        _currentUserService = currentUserService;
        _documentRepository = documentRepository;
    }
    
    public async ValueTask<bool> Handle(DeleteDocumentCommand command, CancellationToken cancellationToken)
    {
        var currentUserId = Guid.Parse(_currentUserService.UserId);
        var curentUserRole = _currentUserService.Role;
        var objectKey = await _documentRepository.GetDocumentObjectKey(command.Id);
        
        var dbResponse = await _documentRepository
            .DeleteDocumentAsync(command.Id, currentUserId, curentUserRole, cancellationToken);
        
        if (!dbResponse)
        {
            throw new ApplicationException(ExceptionMessage);
        }
        
        await _fileService
            .DeleteFileAsync(objectKey);
        
        return dbResponse;
    }
}