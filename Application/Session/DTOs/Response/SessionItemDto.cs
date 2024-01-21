namespace Application.Session.DTOs.Response
{
    public record SessionItemDto(Guid SessionId, 
                                DateTime StartTime, 
                                DateTime EndTime, 
                                UserInfoDto Physician,
                                UserInfoDto Patient,
                                bool IsArchived,
                                bool IsDeleted,
                                int CurrentPainScale,
                                int AveragePainScaleLastMonth,
                                int HighestPainScaleLastMonth);
}
