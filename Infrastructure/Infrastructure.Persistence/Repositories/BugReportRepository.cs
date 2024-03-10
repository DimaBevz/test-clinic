using Application.BugReport.DTOs.RequestDTOs;
using Application.BugReport.DTOs.ResponseDTOs;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class BugReportRepository : IBugReportRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BugReportRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SentBugReportDto?> SendBugReportAsync(
        SendBugReportDto sendBugReportDto, 
        FileDataDto?[] fileDataDtos, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var bugReport = sendBugReportDto.ToBugReport();
        var bugPhotoList = fileDataDtos
            .Select(x =>
            {
                var photo = x!.ToBugPhoto();
                photo.BugReportId = bugReport.Id;
                return photo;
            })
            .ToList();
        bugReport.UserId = userId;

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        await _dbContext.BugReports.AddAsync(bugReport, cancellationToken);
        var bugRowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        await _dbContext.BugReportPhotos.AddRangeAsync(bugPhotoList, cancellationToken);
        var bugPhotosRowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var result = bugRowsAffected > 0 && bugPhotosRowsAffected >= bugPhotoList.Count;
        
        if (!result) return default;
        
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);

        var model = bugReport.ToSentBugReportDto() with { Photos = bugPhotoList.Select(x => x.ToSentBugPhotoDto()).ToList() };
        return model;
    }

    public async Task<List<GetBugReportsDto>> GetBugReportsAsync(CancellationToken cancellationToken)
    {
        var bugReport = await _dbContext.BugReports.Include(x => x.BugReportPhotos).ToListAsync(cancellationToken);
        var result = bugReport.Select(x => x.ToGetBugReportsDto()).ToList();
        return result;
    }
}