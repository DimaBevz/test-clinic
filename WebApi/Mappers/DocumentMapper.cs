using Application.Document.DTOs.RequestDTOs;
using Riok.Mapperly.Abstractions;
using WebApi.DTOs.Document;

namespace WebApi.Mappers;

[Mapper]
internal static partial class DocumentMapper
{
    public static UploadDocumentDto ToUploadDocumentDto(this UploadDocumentFormDto source)
    {
        var uploadDocumentDto = source.ToUploadDocumentDtoStreamless();
        var fs = source.File.OpenReadStream();
        
        uploadDocumentDto = uploadDocumentDto with
        {
            FileStream = fs, ContentType = source.File.ContentType, FileName = source.File.FileName
        };
        
        return uploadDocumentDto;
    }

    public static UploadMultipleDocumentsDto ToUploadMultipleDocumentsDto(this UploadMultipleDocumentsFormDto source)
    {
        
        var streams = source.Files.Select(x => x.OpenReadStream()).ToList();
        var titles = source.Files.Select(x => x.FileName).ToList();
        var contentTypes = source.Files.Select(x => x.ContentType).ToList();
        var documentTypes = source.DocumentTypes.ToList();

        var uploadMultipleDocumentsDto = new UploadMultipleDocumentsDto(
            Streams: streams, 
            ContentTypes: contentTypes, 
            DocumentTypes: documentTypes, 
            FileNames: titles, 
            SessionId: source.SessionId);

        return uploadMultipleDocumentsDto;
    }

    private static partial UploadDocumentDto ToUploadDocumentDtoStreamless(this UploadDocumentFormDto source);
}