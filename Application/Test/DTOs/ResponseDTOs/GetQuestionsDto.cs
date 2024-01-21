namespace Application.Test.DTOs.ResponseDTOs;

public record GetQuestionsDto(string Subtitle, List<GetQuestionDto> Questions);