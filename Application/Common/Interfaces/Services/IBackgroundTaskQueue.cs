using Mediator;

namespace Application.Common.Interfaces.Services
{
    public interface IBackgroundTaskQueue
    {
        Task<ICommand<bool>> DequeueAsync(CancellationToken cancellationToken);
        Task QueueTaskAsync(ICommand<bool> task, CancellationToken cancellationToken = default);
    }
}
