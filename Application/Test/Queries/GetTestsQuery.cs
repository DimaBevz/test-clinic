using Application.Common.Interfaces.Repositories;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Queries;

public record GetTestsQuery() : IQuery<List<GetTestDto>>;

public class GetTestsQueryHandler : IQueryHandler<GetTestsQuery, List<GetTestDto>>
{
    private readonly ITestRepository _testRepository;

    public GetTestsQueryHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public ValueTask<List<GetTestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
    {
        return new ValueTask<List<GetTestDto>>(_testRepository.GetTestsAsync(cancellationToken));
    }
}