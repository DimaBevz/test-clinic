using Application.Position.DTOs.ResponseDTOs;

namespace Application.TimetableTemplate.DTOs
{
    public record PhysicianInfoDto
    (
        string FirstName,
        string LastName,
        string Patronymic,
        string PhotoUrl,
        List<PositionDto> Positions
    );
}
