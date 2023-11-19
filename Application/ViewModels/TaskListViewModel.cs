using System.Collections.ObjectModel;
using Application.Extensions;
using Application.Tasks.Create;
using Application.Tasks.ListByDate;
using Application.TrackedTasks.Create;
using Application.TrackedTasks.Get;
using Application.TrackedTasks.Remove;
using Application.TrackedTasks.Track;
using Application.TrackedTasks.Update;
using Application.TrackedTasks.UpdateRange;
using Domain.Abstractions;
using Domain.Entities.Tasks;
using Domain.ReactiveEntities;
using DynamicData;
using DynamicData.Binding;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;


namespace Application.ViewModels;

public class TaskListViewModel : ViewModelBase
{
    private readonly ISender _sender;

    // private readonly ITaskTrackingService _taskTrackingService;
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

    public TaskListViewModel(ISender sender)
    {
        _sender = sender;
        Tasks = new ObservableCollection<ReactiveTask>();
        _taskDate = DateTime.Today;

        StartTrackingCommand = ReactiveCommand.CreateFromTask<ReactiveTask>(StartTracking);
        StopTrackingCommand = ReactiveCommand.CreateFromTask<ReactiveTask>(StopTracking);
        RemoveCommand = ReactiveCommand.CreateFromTask<ReactiveTask>(RemoveTask);

        this.WhenValueChanged(x => x.TaskDate)
            .Subscribe(date => UpdateShowedTasksByDate(date).Wait());
    }

    private async Task StartTracking(ReactiveTask task)
    {
        var trackTaskCommand = new TrackTaskCommand(task, _cancellationTokenSource.Token);
        await _sender.Send(trackTaskCommand);
        IsTrackingAny = true;
    }

    private async Task StopTracking(ReactiveTask task)
    {
        _cancellationTokenSource.Cancel();
        task.IsTracked = false;
        IsTrackingAny = false;
        _cancellationTokenSource = new CancellationTokenSource();

        var updateTrackedTasksCommand = new UpdateTrackedTasksCommand(Tasks
            .Select(t => t.ToTask())
            .ToList());
        await _sender.Send(updateTrackedTasksCommand);
    }

    private async Task RemoveTask(ReactiveTask task)
    {
        var removeTrackedTaskCommand = new RemoveTrackedTaskCommand(task.Id);
        await _sender.Send(removeTrackedTaskCommand);
        Tasks.Remove(task);
    }

    private async Task UpdateShowedTasksByDate(DateTime date)
    {
        var listTasksByDateQuery = new ListTasksByDateQuery(date);
        var tasks = await _sender.Send(listTasksByDateQuery);

        Tasks.Clear();
        Tasks.AddRange(tasks.Select(t => t.ToReactiveTask()));
    }

    public async Task AddTrackedTask(TrackedTask trackedTask)
    {
        trackedTask.CreatedOn = TaskDate;
        var createTrackedTaskCommand = new CreateTrackedTaskCommand(trackedTask);
        await _sender.Send(createTrackedTaskCommand);

        Tasks.Add(trackedTask.ToReactiveTask());
    }

    public async Task EditTask(Guid taskId, string newContent)
    {
        var updateTrackedTaskCommand = new UpdateTrackedTaskCommand(taskId, newContent);
        await _sender.Send(updateTrackedTaskCommand);
        await UpdateShowedTasksByDate(TaskDate);
    }

    private static IEnumerable<double> AdjustTimeToTarget(IEnumerable<TrackedTask> tasks, double target)
    {
        var currentSpentTime = tasks
            .Select(t => t.TimeWasted.TotalHours)
            .ToList();

        var totalSpentTime = currentSpentTime.Sum();
        var unspentTime = target - totalSpentTime;

        return (from spentTime in currentSpentTime
            let proportion = spentTime / totalSpentTime
            let adjustedTime = unspentTime * proportion
            select spentTime + adjustedTime).ToList();
    }

    public async Task<string> GetExportString()
    {
        var listTasksByDateQuery = new ListTasksByDateQuery(TaskDate);
        var tasks = (await _sender.Send(listTasksByDateQuery)).ToList();

        var adjustedTime = AdjustTimeToTarget(tasks, 8).ToList();

        var exportStrings = tasks
            .Zip(adjustedTime)
            .Select(pair =>
                $"{pair.First.Content}," +
                $" - {Math.Round(pair.Second, 2)}")
            .ToList();

        exportStrings.Add($"Total: {Math.Round(adjustedTime.Sum(), 2)}");

        return string.Join('\n', exportStrings);
    }
}