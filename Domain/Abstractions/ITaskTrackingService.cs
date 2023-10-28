using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskTrackingService
{
    public IReadOnlyList<Task> GetTasks();
}