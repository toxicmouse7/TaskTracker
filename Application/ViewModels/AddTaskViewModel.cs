using System.Reactive;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;

namespace Application.ViewModels;

public class AddTaskViewModel : ViewModelBase
{
    private string _content = null!;
    public string Content 
    { 
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }
    
    public ReactiveCommand<Unit, Task> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        
    public AddTaskViewModel()
    {
        var isValidObservable = this.WhenAnyValue(
            x => x.Content,
            x => !string.IsNullOrWhiteSpace(x));

        OkCommand = ReactiveCommand.Create(
            () => new Task(Guid.NewGuid(), Content), isValidObservable);
        CancelCommand = ReactiveCommand.Create(() => { });
    }
}