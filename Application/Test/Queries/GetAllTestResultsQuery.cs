using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Test.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Test.Queries;

public record GetAllTestResultsQuery(Guid UserId) : IQuery<List<GetAllTestResultsDto>>;

public class GetAllTestResultsQueryHandler : IQueryHandler<GetAllTestResultsQuery, List<GetAllTestResultsDto>>
{
    private readonly ITestRepository _testRepository;

    public GetAllTestResultsQueryHandler(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public ValueTask<List<GetAllTestResultsDto>> Handle(GetAllTestResultsQuery request, CancellationToken cancellationToken)
    {
        return new ValueTask<List<GetAllTestResultsDto>>(_testRepository.GetAllUserTestResultsAsync(request.UserId, cancellationToken));
    }
}