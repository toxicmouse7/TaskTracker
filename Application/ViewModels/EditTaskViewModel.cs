using System.Reactive;
using Domain.ReactiveEntities;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;

namespace Application.ViewModels;

public class EditTaskViewModel : ViewModelBase
{
    private string _content = null!;
    public string Content 
    { 
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }
    
    public ReactiveCommand<Unit, string> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        
    public EditTaskViewModel(ReactiveTask reactiveTask)
    {
        Content = reactiveTask.Content;
        
        var isValidObservable = this.WhenAnyValue(
            x => x.Content,
            x => !string.IsNullOrWhiteSpace(x));

        OkCommand = ReactiveCommand.Create(
            () => Content, isValidObservable);
        CancelCommand = ReactiveCommand.Create(() => { });
    }
}