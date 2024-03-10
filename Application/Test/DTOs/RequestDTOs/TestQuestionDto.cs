namespace Application.Test.DTOs.RequestDTOs;

public record TestQuestionDto(string Text, List<TestOptionDto> Options);