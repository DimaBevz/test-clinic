namespace Application.Document.DTOs.ResponseDTOs;

public record DocumentUploadedDto
(
    Guid Id,
    string Title,
    string PresignedUrl
);