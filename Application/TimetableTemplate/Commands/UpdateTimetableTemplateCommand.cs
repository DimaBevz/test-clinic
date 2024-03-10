using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Extensions;
using Application.TimetableTemplate.DTOs.Request;
using Mediator;
using ApplicationException = Application.Exceptions.ApplicationException;

namespace Application.TimetableTemplate.Commands
{
    public record UpdateTimetableTemplateCommand
        (UpdateTimetableTemplateDto Template) : ICommand<UpdateTimetableTemplateDto>;

    public class UpdateTimetableTemplateCommandHandler : ICommandHandler<UpdateTimetableTemplateCommand, UpdateTimetableTemplateDto>
    {
        private readonly ITimetableRepository _timetableRepository;

        public UpdateTimetableTemplateCommandHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<UpdateTimetableTemplateDto> Handle(UpdateTimetableTemplateCommand command, CancellationToken cancellationToken)
        {
            ValidateTemplate(command.Template);

            var result = await _timetableRepository.UpdateTemplateAsync(command.Template, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException("Wrong params for update");
            }

            return result;
        }

        private static void ValidateTemplate(UpdateTimetableTemplateDto template)
        {
            ValidateDateRange(template.StartDate, template.EndDate, "Start date shouldn't be later");

            foreach (var sessionTemp in template.SessionTemplates.Values)
            {
                if (sessionTemp.IsActive)
                {
                    foreach (var sessionTime in sessionTemp.SessionTimeTemplates)
                    {
                        ValidateDateRange(sessionTime.StartTime, sessionTime.EndTime, "Start time shouldn't be later than end time");
                    }
                }
            }
        }

        private static void ValidateDateRange(DateTime start, DateTime end, string errorMessage)
        {
            var comparisonResult = start.CompareWith(end);

            if (comparisonResult == DateComparisonResult.Later)
            {
                throw new ApplicationException(errorMessage);
            }
        }

        private static void ValidateDateRange(DateOnly start, DateOnly end, string errorMessage)
        {
            var comparisonResult = start.CompareWith(end);

            if (comparisonResult == DateComparisonResult.Later)
            {
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
