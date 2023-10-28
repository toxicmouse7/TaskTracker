using Domain.Primitives;

namespace Domain.Entities.Tasks;

public class Task : Entity<Guid>
{
    public Task(Guid id, string content)
    {
        Id = id;
        Content = content;
        TimeWasted = TimeSpan.Zero;
    }
    
    public string Content { get; set; }
    public TimeSpan TimeWasted { get; set; }
}