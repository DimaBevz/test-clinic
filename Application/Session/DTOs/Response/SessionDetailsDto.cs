namespace Application.Session.DTOs.Response
{
    public record SessionDetailsDto(Guid Id,
                                    DateTime StartTime,
                                    DateTime EndTime,
                                    PhysicianDataDto Physician,
                                    PatientDataDto Patient,
                                    int CurrentPainScale,
                                    int AveragePainScaleLastMonth,
                                    int HighestPainScaleLastMonth,
                                    string Complaints,
                                    string? Treatment,
                                    string? Recommendations,
                                    string? DiagnosisTitle,
                                    Guid? MeetingId,
                                    bool IsArchived,
                                    bool IsDeleted,
                                    List<DocumentItemDto> Documents);
}
