using Application.Common.Interfaces.Repositories;
using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;
using Mediator;

namespace Application.TimetableTemplate.Commands
{
    public record AddTimetableTemplateCommand(AddTimetableTemplateDto Template) : ICommand<AddTimetableTemplateDto>;

    public class AddTimetableTemplateCommandHandler : ICommandHandler<AddTimetableTemplateCommand, AddTimetableTemplateDto>
    {
        private readonly ITimetableRepository _timetableRepository;

        public AddTimetableTemplateCommandHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }
        public async ValueTask<AddTimetableTemplateDto> Handle(AddTimetableTemplateCommand command, CancellationToken cancellationToken)
        {
            var result = await _timetableRepository.CreateTemplateAsync(command.Template, cancellationToken);

            return result;
        }
    }
}
