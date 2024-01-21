using Application.Common.Enums;

namespace WebApi.DTOs.Document;

public record UploadMultipleDocumentsFormDto(
    IFormFile[] Files, 
    DocumentType[] DocumentTypes,
    Guid? SessionId = null);