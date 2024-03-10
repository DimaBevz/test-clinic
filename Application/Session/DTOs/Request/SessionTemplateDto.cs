namespace Application.Session.DTOs.Request
{
    public record SessionTemplateDto
    (
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
