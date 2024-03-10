namespace Application.TimetableTemplate.DTOs
{
    public record DaySessionsDto
    (
        bool IsActive,
        List<SessionTimeTemplateDto> SessionTimeTemplates
    );
}
