using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Comment.Commands;

public record UpdateCommentCommand(UpdateCommentRequestDto RequestDto) : ICommand<UpdateCommentResponseDto>;

public class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand, UpdateCommentResponseDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICurrentUserService _currentUserService;
    private const string ExceptionMessage = "Comment update aborted";
    
    public UpdateCommentCommandHandler(ICommentRepository commentRepository, ICurrentUserService currentUserService)
    {
        _commentRepository = commentRepository;
        _currentUserService = currentUserService;
    }
    public async ValueTask<UpdateCommentResponseDto> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var currentUserId = new Guid(_currentUserService.UserId);
        var result = await _commentRepository.UpdateCommentAsync(
            command.RequestDto,
            currentUserId,
            cancellationToken);
        
        if (result is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }

        return result;
        
    }
}