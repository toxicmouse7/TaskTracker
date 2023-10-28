using Domain.Abstractions;
using Task = Domain.Entities.Tasks.Task;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private static readonly List<Task> Tasks = new()
    {
        new Task(Guid.NewGuid(), "Task 1"),
        new Task(Guid.NewGuid(), "Task 2")
    };
    
    public IReadOnlyList<Task> GetTasks()
    {
        return Tasks;
    }
}