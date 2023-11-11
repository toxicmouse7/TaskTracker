using Domain.ReactiveEntities;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskTrackingService
{
    public Task? GetTaskById(Guid id);
    public IReadOnlyList<Task> GetTasks();
    public IReadOnlyList<Task> GetTasks(DateTime date);
    public void AddTask(Task task);
    public void TrackTask(ReactiveTask task, CancellationToken cancellationToken);
    public void UpdateTasks(IEnumerable<ReactiveTask> reactiveTasks);
    public void UpdateTask(ReactiveTask reactiveTask);
    public void UpdateTask(Task task);
    public void RemoveTask(ReactiveTask task);
}