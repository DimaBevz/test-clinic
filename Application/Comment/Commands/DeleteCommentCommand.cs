using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Comment.Commands;

public record DeleteCommentCommand(Guid Id) : ICommand<bool>;

public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand,bool>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICurrentUserService _currentUserService;
    
    public DeleteCommentCommandHandler(ICommentRepository commentRepository, ICurrentUserService currentUserService)
    {
        _commentRepository = commentRepository;
        _currentUserService = currentUserService;
    }
    
    public async ValueTask<bool> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var currentUserId = new Guid(_currentUserService.UserId);
        var role = _currentUserService.Role;

        var result = await _commentRepository.DeleteCommentAsync(command.Id, currentUserId, role, cancellationToken);

        return result;
    }
}