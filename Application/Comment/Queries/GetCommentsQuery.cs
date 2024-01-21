using Application.Comment.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.Comment.Queries;

public record GetCommentsQuery(Guid PhysicianId) : IQuery<List<GetCommentResponseDto>>;

public class GetCommentsQueryHandler : IQueryHandler<GetCommentsQuery, List<GetCommentResponseDto>>
{
    private readonly ICommentRepository _commentRepository;
    
    public GetCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public ValueTask<List<GetCommentResponseDto>> Handle(GetCommentsQuery query, CancellationToken cancellationToken)
    {
        return new ValueTask<List<GetCommentResponseDto>>(
            _commentRepository.GetCommentsAsync(query.PhysicianId, cancellationToken));
    }
}