using Application.Common.Enums;

namespace Application.Document.DTOs.RequestDTOs
{
    public record UploadDocumentDto(
        Guid? SessionId,
        DocumentType DocumentType,
        string? ContentType = null, 
        string? FileName = null,  
        Stream? FileStream = null);
}
