using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<Task> Tasks { get; set; }
}