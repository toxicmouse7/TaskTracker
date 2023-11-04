using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Tasks.Task;

namespace Domain.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<Task> Tasks { get; set; }
}