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

    public IReadOnlyList<Task> GetTasks(DateTime date)
    {
        return _dbContext.Tasks
            .AsNoTracking()
            .Where(t => t.CreatedOn.Date == date.Date)
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

    public void RemoveTask(Task task)
    {
        _dbContext.Tasks.Remove(task);
    }
}