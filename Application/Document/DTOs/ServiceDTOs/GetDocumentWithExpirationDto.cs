namespace Application.Document.DTOs.ServiceDTOs;

public record GetDocumentWithExpirationDto(
    Guid Id,
    string Title, 
    DateTime ExpiresAt,
    string PresignedUrl);