namespace Application.Comment.DTOs.RequestDTOs;

public record CreateCommentRequestDto
(
    Guid PhysicianId, 
    string CommentText, 
    float Rating
);
