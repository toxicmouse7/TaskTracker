using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Tasks.Task;

namespace Infrastructure;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    public DbSet<Task> Tasks { get; set; } = null!;
}