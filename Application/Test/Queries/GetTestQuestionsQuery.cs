using Application.Common.Interfaces.Repositories;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Queries;

public record GetTestQuestionsQuery(Guid TestId) : IQuery<GetQuestionsDto>;

public class GetTestQuestionsQueryHandler : IQueryHandler<GetTestQuestionsQuery, GetQuestionsDto>
{
    private readonly ITestRepository _testRepository;

    public GetTestQuestionsQueryHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public ValueTask<GetQuestionsDto> Handle(GetTestQuestionsQuery request, CancellationToken cancellationToken)
    {
        return new ValueTask<GetQuestionsDto>(
            _testRepository.GetQuestionsByTestIdAsync(request.TestId,cancellationToken));
    }
}