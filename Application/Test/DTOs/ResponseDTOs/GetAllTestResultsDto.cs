namespace Application.Test.DTOs.ResponseDTOs;

public record GetAllTestResultsDto(Guid Id, string Name, string Description, int TotalScore, string Verdict);