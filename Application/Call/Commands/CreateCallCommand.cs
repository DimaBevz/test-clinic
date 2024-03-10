using Application.Call.DTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Services;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Call.Commands;

public record CreateCallCommand(Guid SessionId): IRequest<string>;

public class CreateCallHandler : IRequestHandler<CreateCallCommand, string>
{
    private readonly ICallService _callService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;
    private readonly ISessionService _sessionService;
    private const string ExceptionSessionMessage = "No such session";
    private const string ExceptionUserIdMessage = "User is not authorized";
    private const string ExceptionUserMessage = "No such user with this Id";

    public CreateCallHandler(ICallService callService, ICurrentUserService currentUserService, ISessionService sessionService, IUserService userService)
    {
        _callService = callService;
        _currentUserService = currentUserService;
        _sessionService = sessionService;
        _userService = userService;
    }
    public async ValueTask<string> Handle(CreateCallCommand request, CancellationToken cancellationToken)
    {
        var meetingId = await _sessionService.GetMeetingId(request.SessionId, cancellationToken);

        if (meetingId is null)
        {
            throw new ApplicationException(ExceptionSessionMessage);
        }

        if (!Guid.TryParse(_currentUserService.UserId, out var userId))
        {
            throw new ApplicationException(ExceptionUserIdMessage);
        }

        var user = await _userService.GetPartialUserAsync(userId);

        if (user is null)
        {
            throw new ApplicationException(ExceptionUserMessage);
        }

        return await _callService.GetCallToken(new CreateCallDto
        {
            Name = $"{user.FirstName} {user.LastName}",
            MeetingId = meetingId.ToString()!,
            Picture = user.PhotoUrl,
            IsHost = user.Role == RoleType.Physician,
            UserId = _currentUserService.UserId
        }) ?? string.Empty;
    }
}
