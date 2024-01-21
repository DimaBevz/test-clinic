using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITimetableRepository
    {
        Task<AddTimetableTemplateDto> CreateTemplateAsync(AddTimetableTemplateDto request, CancellationToken cancellationToken);
        Task<AddTimetableTemplateDto> CreateDefaultTemplateAsync(Guid physicianId, CancellationToken cancellationToken);
        Task<GetTimetablesDto?> GetTimetablesAsync(GetTimetableTemplateDto request, CancellationToken cancellationToken);
        Task<GetAvailableTimetableDto> GetAvailableTimetableAsync(Guid physicianId, CancellationToken cancellationToken);
        Task<UpdateTimetableTemplateDto> UpdateTemplateAsync(UpdateTimetableTemplateDto request, CancellationToken cancellationToken);
        Task<bool> DeleteTemplateAsync(Guid physicianId, CancellationToken cancellationToken);
    }
}
