using Application.Common.Enums;

namespace Application.Document.DTOs.RequestDTOs;

public record UploadMultipleDocumentsDto(
    List<Stream> Streams, 
    List<string> ContentTypes, 
    List<string> FileNames, 
    List<DocumentType> DocumentTypes, 
    Guid? SessionId = null);