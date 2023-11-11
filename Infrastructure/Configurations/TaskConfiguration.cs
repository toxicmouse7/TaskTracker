using Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TrackedTask>
{
    public void Configure(EntityTypeBuilder<TrackedTask> builder)
    {
        
    }
}