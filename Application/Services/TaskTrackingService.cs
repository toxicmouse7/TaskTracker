using Domain.Abstractions;
using Task = Domain.Entities.Tasks.Task;

namespace Application.Services;

public class TaskTrackingService : ITaskTrackingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _taskRepository;

    public TaskTrackingService(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public IReadOnlyList<Task> GetTasks()
    {
        return _taskRepository.GetTasks();
    }

    public void AddTask(Task task)
    {
        _taskRepository.AddTask(task);
        _unitOfWork.SaveChanges();
    }
}