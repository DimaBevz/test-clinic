namespace Application.Comment.DTOs.RequestDTOs;

public record UpdateCommentRequestDto
(
    Guid Id, 
    string CommentText, 
    float Rating
);
