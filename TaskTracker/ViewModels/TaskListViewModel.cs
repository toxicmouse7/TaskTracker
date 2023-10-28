using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Entities.Tasks;


namespace TaskTracker.ViewModels;

public class TaskListViewModel : ViewModelBase
{
    public ObservableCollection<Task> Tasks { get; }
    
    public TaskListViewModel(IEnumerable<Task> tasks)
    {
        Tasks = new ObservableCollection<Task>(tasks);
    }
}