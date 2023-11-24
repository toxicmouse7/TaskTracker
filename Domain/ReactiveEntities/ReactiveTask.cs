using ReactiveUI;

namespace Domain.ReactiveEntities;

public class ReactiveTask : ReactiveObject
{
    private string _content = string.Empty;
    private TimeSpan _timeWasted;
    private bool _isTracked;

    public Guid Id { get; }
    public DateTime CreatedOn { get; init; }

    public bool IsTracked
    {
        get => _isTracked;
        set => this.RaiseAndSetIfChanged(ref _isTracked, value);
    }
    public string Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public TimeSpan TimeWasted
    {
        get => _timeWasted;
        set => this.RaiseAndSetIfChanged(ref _timeWasted, value);
    }

    public ReactiveTask(Guid id, string content, TimeSpan timeWasted, DateTime createdOn)
    {
        Id = id;
        Content = content;
        TimeWasted = timeWasted;
        CreatedOn = createdOn;
    }
    
    
}