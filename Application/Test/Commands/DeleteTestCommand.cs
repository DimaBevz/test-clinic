using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Test.Commands;

public record DeleteTestCommand(Guid Id) : ICommand<bool>;

public class DeleteTestCommandHandler : ICommandHandler<DeleteTestCommand,bool>
{
    private readonly ITestRepository _testRepository;
    
    public DeleteTestCommandHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }
    
    public ValueTask<bool> Handle(DeleteTestCommand command, CancellationToken cancellationToken)
    {
        return new ValueTask<bool>(
            _testRepository.DeleteTestAsync(command.Id, cancellationToken));
    }
}