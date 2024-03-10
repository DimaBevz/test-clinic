using Application.Test.DTOs.RequestDTOs;
using Application.Test.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class TestMapper
{
    public static partial GetTestDto ToGetTestDto(this Test source);
    
    [MapProperty(nameof(@TestResult.Test.Name),nameof(GetAllTestResultsDto.Name))]
    [MapProperty(nameof(@TestResult.Test.Description),nameof(GetAllTestResultsDto.Description))]
    [MapProperty(nameof(@TestResult.TestCriteria.Verdict),nameof(GetAllTestResultsDto.Verdict))]
    public static partial GetAllTestResultsDto ToGetAllTestResultsDto(this TestResult source);

    public static GetQuestionDto ToGetQuestionDto(this TestQuestion source)
    {
        var result = new GetQuestionDto(
            source.Id,
            source.Text,
            source.TestOptions.Select(x => x.ToGetOptionDto()).ToList());
        
        return result;
    }

    public static partial GetOptionDto ToGetOptionDto(this TestOption source);

    public static partial Test ToTest(this CreateTestDto source);

    public static partial TestCriteria ToTestCriteria(this TestCriteriaDto source);
    
    public static partial TestQuestion ToTestQuestion(this TestQuestionDto source);
    
    public static partial TestOption ToTestOption(this TestOptionDto source);

    public static partial CreatedTestDto ToCreatedTestDto(this Test source);


}