using System.Reactive;
using Domain.ReactiveEntities;
using ReactiveUI;

namespace Application.ViewModels;

public class EditTaskViewModel : ViewModelBase
{
    private string _taskContent = null!;
    private string _timeWasted = null!;

    public string TaskContent
    {
        get => _taskContent;
        set => this.RaiseAndSetIfChanged(ref _taskContent, value);
    }

    public string TimeWasted
    {
        get => _timeWasted;
        set => this.RaiseAndSetIfChanged(ref _timeWasted, value);
    }

    public ReactiveCommand<Unit, EditResult> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public EditTaskViewModel(ReactiveTask reactiveTask)
    {
        TaskContent = reactiveTask.Content;
        TimeWasted = reactiveTask.TimeWasted.ToString().Replace(":", " : ");

        var isValidObservable = this.WhenAnyValue(
            x => x.TaskContent,
            x => x.TimeWasted,
            (content, timeWasted) => !string.IsNullOrWhiteSpace(content) &&
                                     TimeSpan.TryParse(timeWasted.Replace(" ", string.Empty), out _));

        OkCommand = ReactiveCommand.Create(
            () => new EditResult(TaskContent, TimeSpan.Parse(TimeWasted.Replace(" ", string.Empty))),
            isValidObservable);
        CancelCommand = ReactiveCommand.Create(() => { });
    }

    public record EditResult(string Content, TimeSpan TimeWasted);
}