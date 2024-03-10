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

    public async Task<List<GetTestDto>> GetTestsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var tests = await _dbContext.Tests.ToListAsync(cancellationToken);
        var models = new List<GetTestDto>();
        foreach (var test in tests)
        {
            var dto = test.ToGetTestDto();
            var isPassed = await _dbContext.TestResults.AnyAsync(tr => tr.PatientDataId == userId && tr.TestId == test.Id,
                cancellationToken);
            models.Add(dto with { IsPassed = isPassed });
        }
        
        var response = models.ToList();
        return response;
    }

    public async Task<GetQuestionsDto> GetQuestionsByTestIdAsync(Guid testId, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Tests.SingleAsync(x => x.Id == testId, cancellationToken);
        var questions = await _dbContext.TestsQuestions
            .Where(x => x.TestId == testId)
            .Include(x => x.TestOptions)
            .ToListAsync(cancellationToken);

        var getQuestionList = questions.Select(x => x.ToGetQuestionDto()).ToList();

        var getQuestions = new GetQuestionsDto(test.Name, test.Subtitle, getQuestionList);
        return getQuestions;
    }

    public async Task<TestProcessedDto?> ProcessTestAsync(
        TestAnswersDto testAnswersDto, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var isAlreadyPassed = await _dbContext.TestResults.AnyAsync(tr => tr.PatientDataId == userId && tr.TestId == testAnswersDto.TestId,
            cancellationToken);
        if (isAlreadyPassed)
            return default;
        
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
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        await _dbContext.TestResults.AddAsync(testResult, cancellationToken);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var rowsFromDetails =
            await FillTestResultDetailsAsync(testResult.Id, testAnswersDto.Answers, cancellationToken);
        var result = rowsAffected > 0 && rowsFromDetails > 0;
        
        if (result)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            return new TestProcessedDto(totalScore, chosenCriteria.Verdict);
        }

        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        return default;
    }

    public async Task<List<GetAllTestResultsDto>> GetAllUserTestResultsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var testResults = await _dbContext.TestResults
            .Where(x => x.PatientDataId == userId)
            .Include(x => x.Test)
            .Include(x => x.TestCriteria)
            .ToListAsync(cancellationToken);

        var result = testResults.Select(x => x.ToGetAllTestResultsDto()).ToList();
        return result;
    }

    public async Task<CreatedTestDto?> CreateTestAsync(CreateTestDto createTestDto, CancellationToken cancellationToken)
    {
        var test = createTestDto.ToTest();
        var testCriteria = createTestDto.Criteria.Select(x =>
            {
                var result = x.ToTestCriteria();
                result.TestId = test.Id;
                return result;
            })
            .ToList();
        var (testQuestions, testOptions) = createTestDto.Questions
            .Aggregate(
                (testQuestions: new List<TestQuestion>(), testOptions: new List<TestOption>()),
                (result, q) =>
                {
                    var question = q.ToTestQuestion();
                    question.TestId = test.Id;
                    
                    result.testQuestions.Add(question);
                    
                    var options = q.Options.Select(x =>
                    {
                        var opt = x.ToTestOption();
                        opt.TestQuestionId = question.Id;
                        return opt;
                    });
                    result.testOptions.AddRange(options);
                    
                    return result;
                });

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        await _dbContext.Tests.AddAsync(test, cancellationToken);
        var rowsTest = await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _dbContext.TestCriteria.AddRangeAsync(testCriteria, cancellationToken);
        var rowsCrit = await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _dbContext.TestsQuestions.AddRangeAsync(testQuestions, cancellationToken);
        var rowsQuest = await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _dbContext.TestOptions.AddRangeAsync(testOptions, cancellationToken);
        var rowsOpt = await _dbContext.SaveChangesAsync(cancellationToken);

        var isSuccess = rowsTest > 0 && rowsCrit > 0 && rowsOpt > 0 && rowsQuest > 0;
        
        if (isSuccess)
        {
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            return test.ToCreatedTestDto();
        }

        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        return default;
    }

    public async Task<bool> DeleteTestAsync(Guid id, CancellationToken cancellationToken)
    {
        var testToRemove = await _dbContext.Tests.Where(x => x.Id == id).SingleAsync(cancellationToken);

        _dbContext.Tests.Remove(testToRemove);

        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var result = rowsAffected > 0;
        
        return result;
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