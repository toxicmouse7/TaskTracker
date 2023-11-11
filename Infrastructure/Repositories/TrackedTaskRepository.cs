using Domain.Abstractions;
using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrackedTaskRepository : ITrackedTaskRepository
{
    private readonly IApplicationDbContext _dbContext;

    public TrackedTaskRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<TrackedTask> GetTasks()
    {
        return _dbContext.Tasks
            .AsNoTracking()
            .OrderBy(t => t.CreatedOn)
            .ToList();
    }

    public IReadOnlyList<TrackedTask> GetTasks(DateTime date)
    {
        return _dbContext.Tasks
            .AsNoTracking()
            .Where(t => t.CreatedOn.Date == date.Date)
            .OrderBy(t => t.CreatedOn)
            .ToList();
    }

    public void AddTask(TrackedTask trackedTask)
    {
        _dbContext.Tasks.Add(trackedTask);
    }

    public async Task AddTaskAsync(TrackedTask trackedTask, CancellationToken cancellationToken = default)
    {
        await _dbContext.Tasks.AddAsync(trackedTask, cancellationToken);
    }

    public void UpdateRange(IEnumerable<TrackedTask> tasks)
    {
        _dbContext.Tasks.UpdateRange(tasks);
    }

    public void Remove(TrackedTask trackedTask)
    {
        _dbContext.Tasks.Remove(trackedTask);
    }

    public void Update(TrackedTask trackedTask)
    {
        _dbContext.Tasks.Update(trackedTask);
    }

    public TrackedTask? GetTask(Guid id)
    {
        return _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
    }
}