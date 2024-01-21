using Application.Common.Interfaces.Repositories;
using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class TestRepository : ITestRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetTestDto>> GetTestsAsync(CancellationToken cancellationToken)
    {
        var tests = await _dbContext.Tests.ToListAsync(cancellationToken);
        var response = tests.Select(x => x.ToGetTestDto()).ToList();
        
        return response;
    }

    public async Task<GetQuestionsDto> GetQuestionsByTestIdAsync(Guid testId, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Tests.SingleAsync(x => x.Id == testId, cancellationToken);
        var questions = await _dbContext.TestsQuestions
            .Include(x => x.TestOptions)
            .Where(x => x.TestId == testId)
            .OrderBy(x => x.Number)
            .ToListAsync(cancellationToken);

        var getQuestionList = questions.Select(x => x.ToGetQuestionDto()).ToList();

        var getQuestions = new GetQuestionsDto(test.Subtitle, getQuestionList);
        return getQuestions;
    }

    public async Task<TestProcessedDto?> ProcessTestAsync(
        TestAnswersDto testAnswersDto, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var totalScore = await GetTotalScoreAsync(testAnswersDto, cancellationToken);
        var testCriteriaList = await _dbContext.TestCriteria
            .Where(x => x.TestId == testAnswersDto.TestId)
            .ToListAsync(cancellationToken);
        
        var chosenCriteria = testCriteriaList
            .Single(x => totalScore >= x.Min && totalScore <= x.Max);

        var testResult = new TestResult
        {
            PatientDataId = userId,
            TestCriteriaId = chosenCriteria.Id,
            TotalScore = totalScore,
            TestId = chosenCriteria.TestId
        };

        await _dbContext.TestResults.AddAsync(testResult, cancellationToken);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var rowsFromDetails =
            await FillTestResultDetailsAsync(testResult.Id, testAnswersDto.AnsweredQuestionDtos, cancellationToken);
        var result = rowsAffected > 0 && rowsFromDetails > 0;
        
        return result ? new TestProcessedDto(totalScore, chosenCriteria.Verdict) : default;
    }

    private async Task<int> GetTotalScoreAsync(TestAnswersDto testAnswersDto, CancellationToken cancellationToken)
    {
        var (testId, answeredQuestionDtos) = testAnswersDto;

        var totalScore = await _dbContext.TestsQuestions
            .Include(x => x.TestOptions)
            .Where(x => x.TestId == testId)
            .SelectMany(x => x.TestOptions)
            .Where(x => answeredQuestionDtos
                .Select(y => y.OptionId)
                .Contains(x.Id))
            .SumAsync(x => x.Points, cancellationToken);

        return totalScore;
    }

    private async Task<int> FillTestResultDetailsAsync(
        Guid testResultId,
        List<AnsweredQuestionDto> answeredQuestionDtos,
        CancellationToken cancellationToken)
    {
        var detailsToAdd = answeredQuestionDtos
            .Select(x => new TestResultDetail()
            {
                TestResultId = testResultId,
                TestOptionId = x.OptionId,
                TestQuestionId = x.QuestionId
            })
            .ToList();
        
        await _dbContext.TestResultDetails.AddRangeAsync(detailsToAdd, cancellationToken);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return rowsAffected;
    }
}