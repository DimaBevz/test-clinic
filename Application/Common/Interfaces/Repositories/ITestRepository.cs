using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories;

public interface ITestRepository
{
    public Task<List<GetTestDto>> GetTestsAsync(CancellationToken cancellationToken);
    public Task<GetQuestionsDto> GetQuestionsByTestIdAsync(Guid testId, CancellationToken cancellationToken);

    public Task<TestProcessedDto?> ProcessTestAsync(
        TestAnswersDto testAnswersDto, 
        Guid userId, 
        CancellationToken cancellationToken);
}