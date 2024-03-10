namespace Application.Session.DTOs.Request
{
    public record UpdateSessionRequestDto
    (
        Guid SessionId, 
        Guid? MeetingId,
        DateTime? StartTime, 
        DateTime? EndTime, 
        Guid? PatientId,
        string? Complaints, 
        string? Treatment, 
        string? Recommendations, 
        string? DiagnosisTitle
    );
}
