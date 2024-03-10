using Application.BugReport.DTOs.RequestDTOs;
using Application.BugReport.DTOs.ResponseDTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.BugReport.Commands;

public record SendBugReportCommand(SendBugReportDto SendBugReportDto) : ICommand<SentBugReportDto>;

public class SendBugReportCommandHandler : ICommandHandler<SendBugReportCommand, SentBugReportDto>
{
    private readonly IBugReportRepository _bugReportRepository;
    private readonly IFileService _fileService;
    private readonly ICurrentUserService _currentUserService;
    private const string ExceptionMessage = "Comment creation aborted";

    public SendBugReportCommandHandler(IBugReportRepository bugReportRepository, IFileService fileService, ICurrentUserService currentUserService)
    {
        _bugReportRepository = bugReportRepository;
        _fileService = fileService;
        _currentUserService = currentUserService;
    }

    public async ValueTask<SentBugReportDto> Handle(SendBugReportCommand command, CancellationToken cancellationToken)
    {
        var userId = new Guid(_currentUserService.UserId);
        var awsUploadResponses = await Task.WhenAll(command.SendBugReportDto.Streams.Select(
                async (x, i) =>
                {
                    var fileName = Guid.NewGuid().ToString();
                    return await _fileService
                        .UploadFileAsync(
                            x,
                            command.SendBugReportDto.ContentTypes[i],
                            FolderName.BugPhotos,
                            fileName);
                }
            )
        );
        
        if (awsUploadResponses.Any(x=>x is null))
        {
            throw new ApplicationException(ExceptionMessage);
        }

        var dbResponse = await _bugReportRepository.SendBugReportAsync(command.SendBugReportDto, awsUploadResponses, userId, cancellationToken);
        
        if (dbResponse is null || dbResponse.Photos.Any(x=> x is null))
        {
            throw new ApplicationException(ExceptionMessage);
        }

        foreach (var stream in command.SendBugReportDto.Streams)
        {
            await stream.DisposeAsync();
        }
        
        return dbResponse;
    }
}