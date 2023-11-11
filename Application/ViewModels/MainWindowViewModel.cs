using System.Reactive;
using System.Reactive.Linq;
using Application.Extensions;
using Application.Services;
using Domain.ReactiveEntities;
using Infrastructure.Repositories;
using ReactiveUI;
using Task = Domain.Entities.Tasks.Task;

namespace Application.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;
    private TaskListViewModel TaskListViewModel { get; }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public ReactiveCommand<ReactiveTask, Unit> EditTaskCommand { get; }
    
    public MainWindowViewModel(TaskListViewModel taskListView)
    {
        TaskListViewModel = taskListView;
        _contentViewModel = TaskListViewModel;

        EditTaskCommand = ReactiveCommand.Create<ReactiveTask>(EditTask);
    }

    private void EditTask(ReactiveTask reactiveTask)
    {
        var editTaskViewModel = new EditTaskViewModel(reactiveTask);
        
        editTaskViewModel.OkCommand.Merge(editTaskViewModel
                .CancelCommand
                .Select(_ => (string?)null))
            .Take(1)
            .Subscribe(newContent =>
            {
                if (newContent != null)
                {
                    TaskListViewModel.EditTask(reactiveTask.Id, newContent);
                }
        
                ContentViewModel = TaskListViewModel;
            });
        
        ContentViewModel = editTaskViewModel;
    }

    public void AddTask()
    {
        var addTaskViewModel = new AddTaskViewModel();

        addTaskViewModel.OkCommand.Merge(addTaskViewModel
                .CancelCommand
                .Select(_ => (Task?)null))
            .Take(1)
            .Subscribe(newItem =>
            {
                if (newItem != null)
                {
                    TaskListViewModel.AddTask(newItem);
                }

                ContentViewModel = TaskListViewModel;
            });

        ContentViewModel = addTaskViewModel;
    }
}