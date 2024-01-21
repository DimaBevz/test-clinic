namespace Application.Document.DTOs.ResponseDTOs;

public record GetDocumentDto(
    Guid Id,
    string Title,
    string PresignedUrl);