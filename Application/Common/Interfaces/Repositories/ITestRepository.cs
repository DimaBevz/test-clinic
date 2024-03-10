using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories;

public interface ITestRepository
{
    public Task<List<GetTestDto>> GetTestsAsync(Guid userId, CancellationToken cancellationToken);
    public Task<GetQuestionsDto> GetQuestionsByTestIdAsync(Guid testId, CancellationToken cancellationToken);

    public Task<TestProcessedDto?> ProcessTestAsync(
        TestAnswersDto testAnswersDto, 
        Guid userId, 
        CancellationToken cancellationToken);

    public Task<List<GetAllTestResultsDto>> GetAllUserTestResultsAsync(Guid userId, CancellationToken cancellationToken);

    public Task<CreatedTestDto?> CreateTestAsync(CreateTestDto createTestDto, CancellationToken cancellationToken);

    public Task<bool> DeleteTestAsync(Guid id, CancellationToken cancellationToken);
}