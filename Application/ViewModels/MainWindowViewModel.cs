using System.Reactive;
using System.Reactive.Linq;
using Domain.Entities.Tasks;
using Domain.ReactiveEntities;
using ReactiveUI;

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
                .Select(_ => (EditTaskViewModel.EditResult?)null))
            .Take(1)
            .Subscribe(editResult =>
            {
                if (editResult != null)
                {
                    TaskListViewModel.EditTask(reactiveTask.Id, editResult.Content, editResult.TimeWasted).Wait();
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
                .Select(_ => (TrackedTask?)null))
            .Take(1)
            .Subscribe(newItem =>
            {
                if (newItem != null)
                {
                    TaskListViewModel.AddTrackedTask(newItem).Wait();
                }

                ContentViewModel = TaskListViewModel;
            });

        ContentViewModel = addTaskViewModel;
    }
}