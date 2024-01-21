using Application.Document.DTOs.RequestDTOs;
using Application.Document.DTOs.ResponseDTOs;
using Application.Document.DTOs.ServiceDTOs;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
internal static partial class DocumentMapper
{
    public static partial GetDocumentDto ToGetDocumentDto(this GetDocumentWithExpirationDto source);
}