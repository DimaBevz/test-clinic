using Application.Common.Interfaces.Services;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Mediator;

namespace Application.User.Commands
{
    public record LoginUserCommand(AuthCredentialsDto Dto) : ICommand<TokenDto>;

    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenDto>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async ValueTask<TokenDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.LoginAsync(command.Dto);
            return authResponse;
        }
    }
}
