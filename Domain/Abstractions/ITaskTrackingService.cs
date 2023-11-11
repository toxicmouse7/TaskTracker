using Domain.Entities.Tasks;
using Domain.ReactiveEntities;

namespace Domain.Abstractions;

public interface ITaskTrackingService
{
    public TrackedTask? GetTaskById(Guid id);
    public IReadOnlyList<TrackedTask> GetTasks();
    public IReadOnlyList<TrackedTask> GetTasks(DateTime date);
    public void AddTask(TrackedTask trackedTask);
    public void TrackTask(ReactiveTask task, CancellationToken cancellationToken);
    public void UpdateTasks(IEnumerable<ReactiveTask> reactiveTasks);
    public void UpdateTask(ReactiveTask reactiveTask);
    public void UpdateTask(TrackedTask trackedTask);
    public void RemoveTask(ReactiveTask task);
}