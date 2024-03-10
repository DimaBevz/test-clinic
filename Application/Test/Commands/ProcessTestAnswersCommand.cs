using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Commands;

public record ProcessTestAnswersCommand(TestAnswersDto TestAnswersDto) : ICommand<TestProcessedDto>;

public class ProcessTestAnswersCommandHandler : ICommandHandler<ProcessTestAnswersCommand, TestProcessedDto>
{
    private readonly ITestRepository _testRepository;
    private readonly ICurrentUserService _currentUserService;
    private const string ExceptionMessage = "Test processing aborted";

    public ProcessTestAnswersCommandHandler(ITestRepository testRepository, ICurrentUserService currentUserService)
    {
        _testRepository = testRepository;
        _currentUserService = currentUserService;
    }

    public async ValueTask<TestProcessedDto> Handle(ProcessTestAnswersCommand command, CancellationToken cancellationToken)
    {
        var userId = new Guid(_currentUserService.UserId);
        var result = await _testRepository.ProcessTestAsync(command.TestAnswersDto, userId, cancellationToken);

        if (result is null)
        {
            throw new ApplicationException(ExceptionMessage);
        }
        
        return result;
    }
}