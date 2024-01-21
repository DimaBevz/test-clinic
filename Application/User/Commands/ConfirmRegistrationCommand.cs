using Application.Common.Interfaces.Services;
using Application.User.DTOs.RequestDTOs;
using Mediator;

namespace Application.User.Commands
{

    public record ConfirmRegistrationCommand(SignUpConfirmationDto Dto) : ICommand<bool>;

    public class ConfirmRegistrationCommandHandler : ICommandHandler<ConfirmRegistrationCommand, bool>
    {
        private readonly IAuthService _authService;

        public ConfirmRegistrationCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async ValueTask<bool> Handle(ConfirmRegistrationCommand command, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.ConfirmSignupAsync(command.Dto);
            return authResponse;
        }
    }
}
