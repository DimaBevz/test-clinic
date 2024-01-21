using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Session.Commands
{
    public record AddSessionCommand(AddSessionDto Dto) : ICommand<AddedSessionDto>;

    public class AddSessionCommandHandler : ICommandHandler<AddSessionCommand, AddedSessionDto>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICallService _callService;
        private const string Patient = "patient";
        private const string ExceptionRoleMessage = "Invalid role";
        private const string ExceptionParamsMessage = "Invalid date and time";
        private const string ExceptionGuidMessage = "Invalid meeting Id from request";

        public AddSessionCommandHandler(ISessionRepository sessionRepository, ICurrentUserService currentUserService, ICallService callService)
        {
            _sessionRepository = sessionRepository;
            _currentUserService = currentUserService;
            _callService = callService;
        }

        public async ValueTask<AddedSessionDto> Handle(AddSessionCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role.ToLower() != Patient)
            {
                throw new ApplicationException(ExceptionRoleMessage);
            }
            var patientId = new Guid(_currentUserService.UserId);
            var meetingString =
                await _callService.CreateMeeting(
                    command.Dto.SessionDate.ToDateTime(TimeOnly.FromDateTime(command.Dto.StartTime)));
            if (!Guid.TryParse(meetingString, out var meetingId))
            {
                throw new ApplicationException(ExceptionGuidMessage);
            }
            var result = await _sessionRepository.AddSessionAsync(command.Dto, patientId, meetingId, cancellationToken);

            if (result == null)
            {
                throw new ApplicationException(ExceptionParamsMessage);
            }

            return result;
        }
    }

}
