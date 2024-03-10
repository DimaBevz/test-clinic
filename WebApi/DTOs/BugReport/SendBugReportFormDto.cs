namespace WebApi.DTOs.BugReport;

public record SendBugReportFormDto(string Topic, string Description, IFormFile[] BugPhotos);