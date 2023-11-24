using System.Collections.ObjectModel;
using Application.Extensions;
using Application.Tasks.ListByDate;
using Application.TrackedTasks.Create;
using Application.TrackedTasks.Remove;
using Application.TrackedTasks.Track;
using Application.TrackedTasks.Update;
using Application.TrackedTasks.UpdateRange;
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

    private CancellationTokenSource _cancellationTokenSource = new();
    private ReactiveTask? _trackedTask;
    private DateTime _taskDate;

    public ReactiveTask? TrackedTask
    {
        get => _trackedTask;
        set => this.RaiseAndSetIfChanged(ref _trackedTask, value);
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
            .Subscribe(date => UpdateShowingTasksByDate(date).Wait());
    }

    private async Task StartTracking(ReactiveTask task)
    {
        var trackTaskCommand = new TrackTaskCommand(task, _cancellationTokenSource.Token);
        await _sender.Send(trackTaskCommand);
        // IsTrackingAny = true;
        TrackedTask = task;
    }

    private async Task StopTracking(ReactiveTask task)
    {
        await _cancellationTokenSource.CancelAsync();
        task.IsTracked = false;
        TrackedTask = null!;
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

    private async Task UpdateShowingTasksByDate(DateTime date)
    {
        var listTasksByDateQuery = new ListTasksByDateQuery(date);
        var tasks = await _sender.Send(listTasksByDateQuery);

        Tasks.Clear();
        Tasks.AddRange(tasks.OrderBy(t => t.CreatedOn).Select(t => t.ToReactiveTask()));

        if (TrackedTask is not null && TrackedTask.CreatedOn.Date == TaskDate.Date)
            Tasks.ReplaceOrAdd(Tasks.First(t => t.Id == TrackedTask.Id), TrackedTask);
    }

    public async Task AddTrackedTask(TrackedTask trackedTask)
    {
        trackedTask.CreatedOn = TaskDate;
        var createTrackedTaskCommand = new CreateTrackedTaskCommand(trackedTask);
        await _sender.Send(createTrackedTaskCommand);

        Tasks.Add(trackedTask.ToReactiveTask());
    }

    public async Task EditTask(Guid taskId, string newContent, TimeSpan newTimeWasted)
    {
        var updateTrackedTaskCommand = new UpdateTrackedTaskCommand(taskId, newContent, newTimeWasted);
        await _sender.Send(updateTrackedTaskCommand);
        await UpdateShowingTasksByDate(TaskDate);
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