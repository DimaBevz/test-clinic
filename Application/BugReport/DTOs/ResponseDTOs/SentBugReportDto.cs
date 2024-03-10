using Application.Common.Enums;

namespace Application.BugReport.DTOs.ResponseDTOs;

public record SentBugReportDto(
    Guid Id, 
    string Topic, 
    string Description, 
    BugReportStatus Status, 
    List<SentBugPhotoDto>? Photos = null);