using Domain.Primitives;

namespace Domain.Entities.Tasks;

public class TrackedTask : Entity<Guid>
{
    public TrackedTask(Guid id, string content)
        : this(id, content, TimeSpan.Zero, DateTime.Now)
    {
    }

    public TrackedTask(Guid id, string content, TimeSpan timeWasted, DateTime createdOn)
    {
        Id = id;
        Content = content;
        TimeWasted = timeWasted;
        CreatedOn = createdOn;
    }
    
    public string Content { get; set; }
    public TimeSpan TimeWasted { get; set; }
    public DateTime CreatedOn { get; set; }
}