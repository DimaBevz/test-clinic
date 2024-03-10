namespace Application.Test.DTOs.ResponseDTOs;

public record GetQuestionsDto(string Name, string Subtitle, List<GetQuestionDto> Questions);