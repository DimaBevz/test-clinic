using Application.Common.Interfaces.Repositories;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Mediator;

namespace Application.User.Commands
{
    public record UpdateUserCommand(UpdateUserDto Dto) : ICommand<GetPartialUserDto>;

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, GetPartialUserDto>
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<GetPartialUserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _repository.UpdateUserAsync(command.Dto);
            return response;
        }
    }
}
