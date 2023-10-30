using System.Collections.ObjectModel;
using Domain.Abstractions;
using Task = Domain.Entities.Tasks.Task;


namespace Application.ViewModels;

public class TaskListViewModel : ViewModelBase
{
    public ObservableCollection<Task> Tasks { get; }
    
    public TaskListViewModel(ITaskTrackingService taskTrackingService)
    {
        Tasks = new ObservableCollection<Task>(taskTrackingService.GetTasks());
    }
}