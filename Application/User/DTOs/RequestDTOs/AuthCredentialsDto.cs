namespace Application.User.DTOs.RequestDTOs
{
    public record AuthCredentialsDto
    (
        string Email,
        string Password
    );
}
