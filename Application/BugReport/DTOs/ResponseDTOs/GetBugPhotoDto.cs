namespace Application.BugReport.DTOs.ResponseDTOs;

public record GetBugPhotoDto(Guid Id, string PresignedUrl);