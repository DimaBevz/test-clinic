using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Comment.Commands;

public record CreateCommentCommand(CreateCommentRequestDto RequestDto) : ICommand<CreateCommentResponseDto>;

public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, CreateCommentResponseDto>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ICurrentUserService _currentUserService;
    private const string ExceptionMessage = "Comment creation aborted";

    public CreateCommentCommandHandler(ICommentRepository commentRepository, ICurrentUserService currentUserService)
    {
        _commentRepository = commentRepository;
        _currentUserService = currentUserService;
    }

    public async ValueTask<CreateCommentResponseDto> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var patientId = new Guid(_currentUserService.UserId);
        
        var result = await _commentRepository.CreateCommentAsync(command.RequestDto, patientId, cancellationToken);
        
        if (result is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }

        return result;
    }
}
