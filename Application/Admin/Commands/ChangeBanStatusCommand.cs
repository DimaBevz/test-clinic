using Application.Admin.DTOs.Response;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Admin.Commands
{
    public record ChangeBanStatusCommand(Guid Id) : ICommand<GetUserItemDto>;

    public class ChangeBanStatusCommandHandler : ICommandHandler<ChangeBanStatusCommand, GetUserItemDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private const string Admin = "admin";
        private const string UserExceptionMessage = "Such user doesn`t exists";
        private const string RoleExceptionMessage = "Current user doesn`t belong to admin role";

        public ChangeBanStatusCommandHandler(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        public async ValueTask<GetUserItemDto> Handle(ChangeBanStatusCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role.ToLower() != Admin)
            {
                throw new ApplicationException(RoleExceptionMessage);
            }

            var result = await _userService.ChangeBanStatusAsync(command.Id, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(UserExceptionMessage);
            }

            return result;
        }
    }
}
