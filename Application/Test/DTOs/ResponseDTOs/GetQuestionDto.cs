namespace Application.Test.DTOs.ResponseDTOs;

public record GetQuestionDto(Guid Id, string Text, List<GetOptionDto> Options);