using Application.BugReport.DTOs.RequestDTOs;
using Application.BugReport.DTOs.ResponseDTOs;
using Application.Common.DTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class BugReportMapper
{
    public static partial BugReport ToBugReport(this SendBugReportDto source);

    [MapProperty(nameof(@FileDataDto.ObjectKey), nameof(BugReportPhoto.PhotoObjectKey))]
    public static partial BugReportPhoto ToBugPhoto(this FileDataDto source);

    public static partial SentBugReportDto ToSentBugReportDto(this BugReport source);

    public static partial SentBugPhotoDto ToSentBugPhotoDto(this BugReportPhoto source);

    public static GetBugReportsDto ToGetBugReportsDto(this BugReport source)
    {
        return new GetBugReportsDto(
            source.Id, 
            source.Topic, 
            source.Description, 
            source.Status,
            source.BugReportPhotos.Select(x => x.ToGetBugPhotosDto()).ToList());
    }

    private static partial GetBugPhotoDto ToGetBugPhotosDto(this BugReportPhoto source);
}