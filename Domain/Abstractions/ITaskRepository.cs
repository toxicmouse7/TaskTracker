using Domain.ReactiveEntities;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface ITaskRepository
{
    public IReadOnlyList<Task> GetTasks();
    public IReadOnlyList<Task> GetTasks(DateTime date);
    public void AddTask(Task task);
    public void UpdateRange(IEnumerable<Task> tasks);
    public void RemoveTask(Task task);
    public void Update(Task task);
    public Task? GetTask(Guid id);
}