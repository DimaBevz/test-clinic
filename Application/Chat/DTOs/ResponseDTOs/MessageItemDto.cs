namespace Application.Chat.DTOs.ResponseDTOs
{
    public record MessageItemDto(Guid UserId, string Message, DateTime CreatedAt, string FirstName, string LastName, string? Patronymic);
}
