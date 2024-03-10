namespace Application.Call.DTOs
{
    public record MeetingDataDto
    (
        Guid? Id,
        string Title,
        DateTime StartedAt,
        DateTime? EndedAt
    );
}
