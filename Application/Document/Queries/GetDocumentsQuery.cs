using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Document.DTOs.ResponseDTOs;
using Application.Mappers;
using Mediator;

namespace Application.Document.Queries;

public record GetDocumentsQuery(Guid UserId) : IQuery<List<GetDocumentDto>>;

public class GetDocumentsQueryHandler : IQueryHandler<GetDocumentsQuery, List<GetDocumentDto>>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IFileService _fileService;
    
    public GetDocumentsQueryHandler(IDocumentRepository documentRepository, IFileService fileService)
    {
        _documentRepository = documentRepository;
        _fileService = fileService;
    }
    
    public async ValueTask<List<GetDocumentDto>> Handle(GetDocumentsQuery query, CancellationToken cancellationToken)
    {
        var documents = await 
            _documentRepository.GetDocumentsAsync(query.UserId, cancellationToken);

        foreach (var document in documents.Where(document => document.ExpiresAt < DateTime.UtcNow))
        {
            var objectKey = await _documentRepository.GetDocumentObjectKey(document.Id);
            var updatedFile = await _fileService.UpdatePresignedLinkAsync(objectKey);
            await _documentRepository
                .UpdatePresignedLinkAsync(
                    document.Id, 
                    updatedFile.ExpiresAt,
                    updatedFile.PresignedUrl, 
                    cancellationToken);
        }
            
        var result = documents.Select(x => x.ToGetDocumentDto()).ToList();
        
        return result;
    }
}