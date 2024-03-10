using Application.Common.Enums;

namespace Application.BugReport.DTOs.ResponseDTOs;

public record GetBugReportsDto(
    Guid Id, 
    string Topic, 
    string Description, 
    BugReportStatus Status, 
    List<GetBugPhotoDto>? Photos = null);