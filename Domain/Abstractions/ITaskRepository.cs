using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskRepository
{
    public IReadOnlyList<Task> GetTasks();
}