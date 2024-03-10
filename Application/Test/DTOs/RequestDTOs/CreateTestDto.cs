using Application.Common.Enums;

namespace Application.Test.DTOs.RequestDTOs;

public record CreateTestDto(
    string Name, 
    TestType Type, 
    string Description, 
    string Subtitle,
    List<TestCriteriaDto> Criteria,
    List<TestQuestionDto> Questions);