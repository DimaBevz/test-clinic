using Application.Common.Enums;

namespace WebApi.DTOs.Document;

public record UploadDocumentFormDto(
    IFormFile File, 
    DocumentType DocumentType,
    Guid? SessionId = null);