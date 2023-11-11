using Domain.Entities.Tasks;
using Domain.ReactiveEntities;

namespace Domain.Abstractions;

public interface ITrackedTaskRepository
{
    public IReadOnlyList<TrackedTask> GetTasks();
    public IReadOnlyList<TrackedTask> GetTasks(DateTime date);
    public void AddTask(TrackedTask trackedTask);
    public System.Threading.Tasks.Task AddTaskAsync(TrackedTask trackedTask, CancellationToken cancellationToken = default);
    public void UpdateRange(IEnumerable<TrackedTask> tasks);
    public void Remove(TrackedTask trackedTask);
    public void Update(TrackedTask trackedTask);
    public TrackedTask? GetTask(Guid id);
}