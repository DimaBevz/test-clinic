using Application.Common.Interfaces.Repositories;
using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Commands;

public record CreateTestCommand(CreateTestDto CreateTestDto) : ICommand<CreatedTestDto>;

public class CreateTestCommandCommandHandler : ICommandHandler<CreateTestCommand, CreatedTestDto>
{
    private readonly ITestRepository _testRepository;
    private const string ExceptionMessage = "Test creating aborted";

    public CreateTestCommandCommandHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public async ValueTask<CreatedTestDto> Handle(CreateTestCommand command, CancellationToken cancellationToken)
    {
        var result = await _testRepository.CreateTestAsync(command.CreateTestDto, cancellationToken);

        if (result is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }
        
        return result;
    }
}