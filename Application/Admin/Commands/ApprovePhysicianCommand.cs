using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.Common.Interfaces.Services;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.Admin.Commands
{
    public record ApprovePhysicianCommand(ApprovePhysicianDto Physician) : ICommand<ApprovedPhysicianDto>;

    public class ApprovePhysicianCommandHandler : ICommandHandler<ApprovePhysicianCommand, ApprovedPhysicianDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private const string Admin = "admin";
        private const string ExceptionPhysicianMessage = "Such physician doesn`t exists";
        private const string RoleExceptionMessage = "Current user doesn`t belong to admin role";

        public ApprovePhysicianCommandHandler(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        public async ValueTask<ApprovedPhysicianDto> Handle(ApprovePhysicianCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role.ToLower() != Admin)
            {
                throw new ApplicationException(RoleExceptionMessage);
            }

            var result =
                await _userService.ApprovePhysicianAsync(command.Physician, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(ExceptionPhysicianMessage);
            }

            return result;
        }
    }
}
