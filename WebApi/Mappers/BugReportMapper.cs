using Application.BugReport.DTOs.RequestDTOs;
using Riok.Mapperly.Abstractions;
using WebApi.DTOs.BugReport;

namespace WebApi.Mappers;

[Mapper]
internal static partial class BugReportMapper
{
    public static SendBugReportDto ToSendBugReportDto(this SendBugReportFormDto source)
    {
        var streams = source.BugPhotos.Select(x => x.OpenReadStream()).ToList();
        var contentTypes = source.BugPhotos.Select(x => x.ContentType).ToList();
        var sendBugReportDto = new SendBugReportDto(source.Topic, source.Description, contentTypes, streams);
        return sendBugReportDto;
    }
}