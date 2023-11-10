using System.Collections.ObjectModel;
using System.Reactive;
using Application.Extensions;
using Domain.Abstractions;
using Domain.ReactiveEntities;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;


namespace Application.ViewModels;

public class TaskListViewModel : ViewModelBase
{
    private readonly ITaskTrackingService _taskTrackingService;
    private CancellationTokenSource _cancellationTokenSource = new();
    private bool _isTrackingAny;
    private DateTime _taskDate;

    public bool IsTrackingAny
    {
        get => _isTrackingAny;
        set => this.RaiseAndSetIfChanged(ref _isTrackingAny, value);
    }
    
    public DateTime TaskDate
    {
        get => _taskDate;
        set => this.RaiseAndSetIfChanged(ref _taskDate, value);
    }

    public ObservableCollection<ReactiveTask> Tasks { get; }
    public ReactiveCommand<ReactiveTask, Unit> StartTrackingCommand { get; }
    public ReactiveCommand<ReactiveTask, Unit> StopTrackingCommand { get; }
    public ReactiveCommand<ReactiveTask, Unit> RemoveCommand { get; }

    public TaskListViewModel(ITaskTrackingService taskTrackingService)
    {
        _taskTrackingService = taskTrackingService;
        Tasks = new ObservableCollection<ReactiveTask>();
        _taskDate = DateTime.Today;

        StartTrackingCommand = ReactiveCommand.Create<ReactiveTask>(StartTracking);
        StopTrackingCommand = ReactiveCommand.Create<ReactiveTask>(StopTracking);
        RemoveCommand = ReactiveCommand.Create<ReactiveTask>(RemoveTask);

        this.WhenValueChanged(x => x.TaskDate)
            .Subscribe(SwitchDate);
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

    public void RemoveTask(ReactiveTask task)
    {
        _taskTrackingService.RemoveTask(task);
        Tasks.Remove(task);
    }

    private void SwitchDate(DateTime date)
    {
        Tasks.Clear();
        Tasks.AddRange(_taskTrackingService
            .GetTasks(date)
            .Select(t => t.ToReactiveTask()));
    }

    public void AddTask(Task task)
    {
        task.CreatedOn = TaskDate;
        _taskTrackingService.AddTask(task);
        Tasks.Add(task.ToReactiveTask());
    }
}