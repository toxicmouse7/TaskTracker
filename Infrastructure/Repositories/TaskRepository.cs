using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
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
        return _dbContext.Tasks
            .AsNoTracking()
            .OrderBy(t => t.CreatedOn)
            .ToList();
    }

    public void AddTask(Task task)
    {
        _dbContext.Tasks.Add(task);
    }

    public void UpdateRange(IEnumerable<Task> tasks)
    {
        _dbContext.Tasks.UpdateRange(tasks);
    }
}