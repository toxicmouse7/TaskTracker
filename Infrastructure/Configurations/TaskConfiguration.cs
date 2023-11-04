using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Tasks.Task;

namespace Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasData(
            new Task(Guid.NewGuid(), "Task 1"),
            new Task(Guid.NewGuid(), "Task 2"));
    }
}