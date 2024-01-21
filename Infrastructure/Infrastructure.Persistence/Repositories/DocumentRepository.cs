using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Document.DTOs.ResponseDTOs;
using Application.Document.DTOs.ServiceDTOs;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DocumentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DocumentUploadedDto?> AddDocumentInfoAsync(
        string title, 
        DocumentType documentType, 
        Guid userId, 
        FileDataDto fileDataDto,
        Guid? sessionId,
        CancellationToken cancellationToken)
    {
        var documentToAdd = fileDataDto.ToDocument();
        documentToAdd.Title = title;
        documentToAdd.Type = documentType;
        documentToAdd.UserId = userId;
        documentToAdd.SessionDetailSessionId = sessionId;

        await _dbContext.Documents.AddAsync(documentToAdd, cancellationToken);
       
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        var result = rowsAffected > 0;
        
        return result ? documentToAdd.ToDocumentUploadedDto() : default;
    }

    public async Task<List<DocumentUploadedDto?>?> AddMultipleDocumentsInfoAsync(List<string> titles,
        List<DocumentType> documentTypes,
        Guid userId,
        FileDataDto?[] fileDataDtos,
        Guid? sessionId,
        CancellationToken cancellationToken)
    {
        var documentsToAdd = fileDataDtos.Select((x, i) =>
        {
            var returnModel = x!.ToDocument();
            returnModel.Title = titles[i];
            returnModel.Type = documentTypes[i];
            returnModel.UserId = userId;
            returnModel.SessionDetailSessionId = sessionId;
            return returnModel;
        }).ToList();

        await _dbContext.Documents.AddRangeAsync(documentsToAdd, cancellationToken);
        
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        var result = rowsAffected > 0;
        
        return result ? documentsToAdd.Select(x=>x.ToDocumentUploadedDto()).ToList() : default;
    }

    public async Task<List<GetDocumentWithExpirationDto>> GetDocumentsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Documents
            .Where(x => x.UserId == userId)
            .Select(x => x.ToGetDocumentDto())
            .ToListAsync(cancellationToken);
        
        return result;
    }

    public async Task<GetDocumentWithExpirationDto?> UpdatePresignedLinkAsync(
        Guid documentId, 
        DateTime expiresAt,
        string presignedLink, 
        CancellationToken cancellationToken)
    {
        var document = await _dbContext.Documents
            .Where(x => x.Id == documentId)
            .FirstAsync(cancellationToken: cancellationToken);
        
        document.PresignedUrl = presignedLink;
        document.ExpiresAt = expiresAt;
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var result = rowsAffected > 0;

        return result ? document.ToGetDocumentDto() : default;
    }

    public async Task<bool> DeleteDocumentAsync(
        Guid id,
        Guid currentUserId,
        string role,
        CancellationToken cancellationToken)
    {
        var documentToDelete = await _dbContext.Documents
            .Where(x => x.Id == id)
            .FirstAsync(cancellationToken);

        if (documentToDelete.UserId != currentUserId && role != nameof(RoleType.Admin)) return false;
        _dbContext.Documents.Remove(documentToDelete);

        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var result = rowsAffected > 0;
            
        return result;
    }

    public async Task<string> GetDocumentObjectKey(Guid id)
    {
        var document = await _dbContext.Documents.SingleAsync(x => x.Id == id);
        return document.DocumentObjectKey;
    }
}