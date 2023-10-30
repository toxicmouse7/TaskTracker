using Domain.Abstractions;
using Task = Domain.Entities.Tasks.Task;

namespace Application.Services;

public class TaskTrackingService : ITaskTrackingService
{
    private ITaskRepository _taskRepository;

    public TaskTrackingService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public IReadOnlyList<Task> GetTasks()
    {
        return _taskRepository.GetTasks();
    }

    public void AddTask(Task task)
    {
        _taskRepository.AddTask(task);
    }
}