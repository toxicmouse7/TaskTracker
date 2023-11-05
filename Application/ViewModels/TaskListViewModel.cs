using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using Application.Extensions;
using Domain.Abstractions;
using Domain.ReactiveEntities;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;


namespace Application.ViewModels;

public class TaskListViewModel : ViewModelBase
{
    private readonly ITaskTrackingService _taskTrackingService;
    private CancellationTokenSource _cancellationTokenSource = new();
    private bool _isTrackingAny;

    public bool IsTrackingAny
    {
        get => _isTrackingAny;
        set => this.RaiseAndSetIfChanged(ref _isTrackingAny, value);
    }

    public ObservableCollection<ReactiveTask> Tasks { get; }
    public ReactiveCommand<ReactiveTask, Unit> StartTrackingCommand { get; }
    public ReactiveCommand<ReactiveTask, Unit> StopTrackingCommand { get; }

    public TaskListViewModel(ITaskTrackingService taskTrackingService)
    {
        _taskTrackingService = taskTrackingService;
        Tasks = new ObservableCollection<ReactiveTask>(taskTrackingService.GetTasks()
            .Select(t => t.ToReactiveTask()));

        StartTrackingCommand = ReactiveCommand.Create<ReactiveTask>(StartTracking);
        StopTrackingCommand = ReactiveCommand.Create<ReactiveTask>(StopTracking);
    }

    public void StartTracking(ReactiveTask task)
    {
        task.IsTracked = true;
        _taskTrackingService.TrackTask(task, _cancellationTokenSource.Token);
        IsTrackingAny = true;
    }

    public void StopTracking(ReactiveTask task)
    {
        _cancellationTokenSource.Cancel();
        task.IsTracked = false;
        IsTrackingAny = false;
        _cancellationTokenSource = new CancellationTokenSource();
        _taskTrackingService.UpdateTasks(Tasks.ToList());
    }
}