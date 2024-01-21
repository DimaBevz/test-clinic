using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Document.DTOs.ResponseDTOs;
using Application.Document.DTOs.ServiceDTOs;

namespace Application.Common.Interfaces.Repositories;

public interface IDocumentRepository
{
    public Task<DocumentUploadedDto?> AddDocumentInfoAsync(
        string title, 
        DocumentType type, 
        Guid userId, 
        FileDataDto fileDataDto,
        Guid? sessionId,
        CancellationToken cancellationToken);

    public Task<List<DocumentUploadedDto?>?> AddMultipleDocumentsInfoAsync(List<string> titles,
        List<DocumentType> documentTypes,
        Guid userId,
        FileDataDto?[] fileDataDtos,
        Guid? sessionId,
        CancellationToken cancellationToken);

    public Task<List<GetDocumentWithExpirationDto>> GetDocumentsAsync(Guid userId, CancellationToken cancellationToken);

    public Task<GetDocumentWithExpirationDto?> UpdatePresignedLinkAsync(
        Guid documentId, 
        DateTime expiresAt,
        string presignedLink, 
        CancellationToken cancellationToken);

    public Task<bool> DeleteDocumentAsync(
        Guid id, 
        Guid currentUserId,
        string role,
        CancellationToken cancellationToken);

    public Task<string> GetDocumentObjectKey(Guid id);
}