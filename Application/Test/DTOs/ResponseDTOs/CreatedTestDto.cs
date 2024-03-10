using Application.Common.Enums;

namespace Application.Test.DTOs.ResponseDTOs;

public record CreatedTestDto(
    Guid Id,
    string Name,
    string Description,
    TestType Type);