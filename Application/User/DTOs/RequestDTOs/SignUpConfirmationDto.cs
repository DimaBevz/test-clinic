namespace Application.User.DTOs.RequestDTOs
{
    public record SignUpConfirmationDto
    (
        string Email,
        string Code
    );
}
