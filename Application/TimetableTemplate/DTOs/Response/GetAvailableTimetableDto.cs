namespace Application.TimetableTemplate.DTOs.Response
{
    public record GetAvailableTimetableDto(Dictionary<DateOnly, List<SessionTimeTemplateDto>> AvailableSessions,
                                           PhysicianInfoDto Physician);
}
