using Application.Common.Enums;

namespace Application.Physician.DTOs.RequestDTOs
{
    public record GetPhysiciansByParamsDto
    (
        int Page,
        int Limit,
        bool IsAscending,
        bool? IsApproved,
        string? Search,
        SortField? SortField,
        List<UserGender>? Sex,
        List<EvaluationType>? Rating,
        int? Experience
    );
}
