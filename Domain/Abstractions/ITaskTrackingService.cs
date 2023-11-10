using Domain.ReactiveEntities;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskTrackingService
{
    public IReadOnlyList<Task> GetTasks();
    public IReadOnlyList<Task> GetTasks(DateTime date);
    public void AddTask(Task task);
    public void TrackTask(ReactiveTask task, CancellationToken cancellationToken);
    public void UpdateTasks(IEnumerable<ReactiveTask> reactiveTasks);
    public void RemoveTask(ReactiveTask task);
}