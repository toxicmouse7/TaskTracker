using Domain.Abstractions;
using Task = Domain.Entities.Tasks.Task;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IApplicationDbContext _dbContext;

    public TaskRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Task> GetTasks()
    {
        return _dbContext.Tasks.ToList();
    }

    public void AddTask(Task task)
    {
        _dbContext.Tasks.Add(task);
    }
}