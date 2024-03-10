namespace Application.Comment.DTOs.ResponseDTOs;

public record UpdateCommentResponseDto
(
    Guid Id, 
    Guid AuthorId,  
    string CommentText, 
    float Rating, 
    DateTime CreatedAt,
    string FirstName, 
    string LastName
);
