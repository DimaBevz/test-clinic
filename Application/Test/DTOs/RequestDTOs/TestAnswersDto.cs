namespace Application.Test.DTOs.RequestDTOs;

public record TestAnswersDto(Guid TestId, List<AnsweredQuestionDto> Answers);