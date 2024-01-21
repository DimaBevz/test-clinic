using Application.Common.Interfaces.Repositories;
using Mediator;

namespace Application.TimetableTemplate.Commands
{
    public record DeleteTimetableTemplateCommand(Guid PhysicianId) : ICommand<bool>;

    public class DeleteTimetableTemplateCommandHandler : ICommandHandler<DeleteTimetableTemplateCommand, bool>
    {
        private readonly ITimetableRepository _timetableRepository;

        public DeleteTimetableTemplateCommandHandler(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<bool> Handle(DeleteTimetableTemplateCommand command, CancellationToken cancellationToken)
        {
            var result = await _timetableRepository.DeleteTemplateAsync(command.PhysicianId, cancellationToken);

            return result;
        }
    }
}
