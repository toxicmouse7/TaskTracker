using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Abstractions;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public int SaveChanges();
    public ChangeTracker ChangeTracker { get; }
}