namespace Application.Test.DTOs.ResponseDTOs;

public record GetQuestionDto(Guid Id, string Text, int Number, List<GetOptionDto> GetOptionDtos);