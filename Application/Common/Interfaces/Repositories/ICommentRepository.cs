using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<CreateCommentResponseDto?> CreateCommentAsync(
        CreateCommentRequestDto createCommentRequestDto,
        Guid patientId, 
        CancellationToken cancellationToken);

    public Task<List<GetCommentResponseDto>> GetCommentsAsync(
        Guid physicianId, 
        CancellationToken cancellationToken);

    public Task<bool> DeleteCommentAsync(
        Guid id, 
        Guid currentUserId, 
        string role,
        CancellationToken cancellationToken);

    public Task<UpdateCommentResponseDto?> UpdateCommentAsync(
        UpdateCommentRequestDto updateCommentRequestDto, 
        Guid currentUserId,
        CancellationToken cancellationToken);
}