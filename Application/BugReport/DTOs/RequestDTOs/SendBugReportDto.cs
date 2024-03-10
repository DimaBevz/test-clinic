namespace Application.BugReport.DTOs.RequestDTOs;

public record SendBugReportDto(
    string Topic, 
    string Description, 
    List<string> ContentTypes, 
    List<Stream> Streams);