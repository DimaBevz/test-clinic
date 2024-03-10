using Application.Common.Enums;

namespace Application.Admin.DTOs.Response
{
    public record GetUserItemDto
    (
        Guid Id,
        string LastName,
        string FirstName,
        string? Patronymic,
        string? PhotoUrl,
        RoleType Role,
        bool IsBanned
    );
}
