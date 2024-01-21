using System.Threading.Channels;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Infrastructure.Services
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<ICommand<bool>> _queue = Channel.CreateUnbounded<ICommand<bool>>();
        public async Task<ICommand<bool>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }

        public async Task QueueTaskAsync(ICommand<bool> task, CancellationToken cancellationToken = default)
        {
            await _queue.Writer.WriteAsync(task);
        }
    }
}
