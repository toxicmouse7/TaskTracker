using Domain.Primitives;

namespace Domain.Entities.Tasks;

public class Task : Entity<Guid>
{
    public Task(Guid id, string content)
        : this(id, content, TimeSpan.Zero)
    {
    }

    public Task(Guid id, string content, TimeSpan timeWasted)
    {
        Id = id;
        Content = content;
        TimeWasted = timeWasted;
        CreatedOn = DateTime.Now;
    }
    
    public string Content { get; set; }
    public TimeSpan TimeWasted { get; set; }
    public DateTime CreatedOn { get; set; }
}