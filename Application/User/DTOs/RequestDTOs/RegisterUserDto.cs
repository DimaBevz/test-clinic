using Application.Common.Enums;

namespace Application.User.DTOs.RequestDTOs
{
    public record RegisterUserDto
    (
        string Email,
        string FirstName,
        string LastName,
        string? Patronymic,
        string PhoneNumber,
        string Password,
        RoleType Role
    );
}
