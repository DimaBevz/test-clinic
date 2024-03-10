using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Queries;

public record GetTestsQuery() : IQuery<List<GetTestDto>>;

public class GetTestsQueryHandler : IQueryHandler<GetTestsQuery, List<GetTestDto>>
{
    private readonly ITestRepository _testRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetTestsQueryHandler(ITestRepository testRepository, ICurrentUserService currentUserService)
    {
        _testRepository = testRepository;
        _currentUserService = currentUserService;
    }

    public ValueTask<List<GetTestDto>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
    {
        var userId = new Guid(_currentUserService.UserId);
        return new ValueTask<List<GetTestDto>>(_testRepository.GetTestsAsync(userId, cancellationToken));
    }
}