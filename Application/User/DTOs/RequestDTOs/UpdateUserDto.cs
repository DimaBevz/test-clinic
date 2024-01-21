namespace Application.User.DTOs.RequestDTOs
{
    public record UpdateUserDto
    ( 
        Guid Id, 
        string? Email, 
        string FirstName, 
        string LastName, 
        string? Patronymic,
        string PhoneNumber,
        DateOnly? Birthday,
        string? Sex
    );
}
