using Application.Services;
using Infrastructure.Repositories;

namespace TaskTracker.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public TaskListViewModel TaskList { get; }
    
    public MainWindowViewModel()
    {
        var taskTrackingService = new TaskTrackingService(new TaskRepository());
        TaskList = new TaskListViewModel(taskTrackingService.GetTasks());
    }
}