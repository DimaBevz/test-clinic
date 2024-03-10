namespace Application.Call.DTOs
{
    public record UpdatedMeetingDataDto
    (
        Guid SessionId,
        string Title,
        DateTime StartedAt,
        DateTime? EndedAt = null,
        Guid? MeetingId = null
    );
}
