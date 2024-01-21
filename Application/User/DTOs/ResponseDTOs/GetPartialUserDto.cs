using Application.Common.Enums;

namespace Application.User.DTOs.ResponseDTOs
{
    public record GetPartialUserDto(
        Guid Id,
        string? Email,
        string FirstName,
        string LastName,
        string? Patronymic,
        string PhoneNumber,
        DateOnly? Birthday,
        string? Sex,
        RoleType Role,
        string? PhotoUrl
    );
}
