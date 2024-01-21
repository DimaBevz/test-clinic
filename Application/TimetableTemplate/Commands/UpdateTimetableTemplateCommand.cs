using Application.Common.Interfaces.Repositories;
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
        private const string ExceptionMessage = "Wrong params for update";

        public UpdateTimetableTemplateCommandHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<UpdateTimetableTemplateDto> Handle(UpdateTimetableTemplateCommand command, CancellationToken cancellationToken)
        {
            var result = await _timetableRepository.UpdateTemplateAsync(command.Template, cancellationToken);

            if (result is null)
            {
                throw new ApplicationException(ExceptionMessage);
            }

            return result;
        }
    }
}
