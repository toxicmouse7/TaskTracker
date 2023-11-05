using System.Reactive.Linq;
using Application.Extensions;
using Application.Services;
using Domain.Abstractions;
using Infrastructure.Repositories;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;

namespace Application.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ITaskTrackingService _taskTrackingService;
    private ViewModelBase _contentViewModel;
    private TaskListViewModel TaskListViewModel { get; }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    
    public MainWindowViewModel(
        TaskListViewModel taskListView,
        ITaskTrackingService taskTrackingService)
    {
        TaskListViewModel = taskListView;
        _taskTrackingService = taskTrackingService;
        _contentViewModel = TaskListViewModel;
    }

    public void AddTask()
    {
        var addItemViewModel = new AddTaskViewModel();

        addItemViewModel.OkCommand.Merge(addItemViewModel.CancelCommand.Select(_ => (Task?)null))
            .Take(1)
            .Subscribe(newItem =>
            {
                if (newItem != null)
                {
                    _taskTrackingService.AddTask(newItem);
                    TaskListViewModel.Tasks.Add(newItem.ToReactiveTask());
                }

                ContentViewModel = TaskListViewModel;
            });

        ContentViewModel = addItemViewModel;
    }
}