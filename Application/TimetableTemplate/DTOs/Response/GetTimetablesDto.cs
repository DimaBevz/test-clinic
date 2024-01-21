namespace Application.TimetableTemplate.DTOs.Response
{
    public record GetTimetablesDto(DateOnly StartDate,
                                   DateOnly EndDate,
                                   Dictionary<DayOfWeek, DaySessionsDto> SessionTemplates,
                                   Guid PhysicianDataId,
                                   bool IsEditable);
}
