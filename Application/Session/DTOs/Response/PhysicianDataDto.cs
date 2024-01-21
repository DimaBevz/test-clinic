using Application.Position.DTOs.ResponseDTOs;

namespace Application.Session.DTOs.Response
{
    public record PhysicianDataDto(Guid Id,
                                   string LastName,
                                   string FirstName,
                                   string? Patronymic,
                                   string? PhotoUrl,
                                   string? Bio,
                                   List<PositionDto>? Specialties = null);
}
