using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions;

public interface IApplicationDbContext
{
    public DbSet<TrackedTask> Tasks { get; set; }
}