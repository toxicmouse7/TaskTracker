using MediatR;

namespace Application.TrackedTasks.Track;

public class TrackTaskCommandHandler : IRequestHandler<TrackTaskCommand>
{
    public Task Handle(TrackTaskCommand request, CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            request.ReactiveTask.IsTracked = true;
            while (!request.CancellationToken.IsCancellationRequested)
            {
                request.ReactiveTask.TimeWasted = request.ReactiveTask.TimeWasted.Add(TimeSpan.FromSeconds(1));
                Thread.Sleep(1000);
            }

            request.ReactiveTask.IsTracked = false;
        }, cancellationToken);

        return Task.CompletedTask;
    }
}