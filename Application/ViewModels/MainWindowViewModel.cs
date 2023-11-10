using System.Reactive.Linq;
using Application.Extensions;
using Application.Services;
using Infrastructure.Repositories;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;

namespace Application.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    private DateTime _taskDate;
    private TaskListViewModel TaskListViewModel { get; }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    
    public MainWindowViewModel(TaskListViewModel taskListView)
    {
        TaskListViewModel = taskListView;
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
                    TaskListViewModel.AddTask(newItem);
                }

                ContentViewModel = TaskListViewModel;
            });

        ContentViewModel = addItemViewModel;
    }
}