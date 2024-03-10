namespace Application.Chat.DTOs.RequestDTOs
{
    public record AddMessageDto
    (
        Guid SessionId, 
        Guid UserId, 
        string Message, 
        DateTime CreatedAt
    );
}