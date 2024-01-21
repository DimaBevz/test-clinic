using Application.Common.DTOs;
using Application.Document.DTOs.ResponseDTOs;
using Application.Document.DTOs.ServiceDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class DocumentMapper
{
    [MapProperty(nameof(FileDataDto.ObjectKey),nameof(Document.DocumentObjectKey))]
    public static partial Document ToDocument(this FileDataDto source);

    public static partial DocumentUploadedDto? ToDocumentUploadedDto(this Document source);
    
    public static partial GetDocumentWithExpirationDto? ToGetDocumentDto(this Document source);
}