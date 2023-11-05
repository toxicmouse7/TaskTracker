using Domain.ReactiveEntities;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskTrackingService
{
    public IReadOnlyList<Task> GetTasks();
    public void AddTask(Task task);
    public void TrackTask(ReactiveTask task, CancellationToken cancellationToken);
    public void UpdateTasks(IEnumerable<ReactiveTask> reactiveTasks);
}