using Application.Extensions;
using Domain.Abstractions;
using Domain.ReactiveEntities;
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

    public void TrackTask(ReactiveTask task, CancellationToken cancellationToken)
    {
        System.Threading.Tasks.Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                task.TimeWasted = task.TimeWasted.Add(TimeSpan.FromSeconds(1));
                Thread.Sleep(1000);
            }
        }, cancellationToken);
    }

    public void UpdateTasks(IEnumerable<ReactiveTask> reactiveTasks)
    {
        _taskRepository.UpdateRange(reactiveTasks.Select(t => t.ToTask()));
        _unitOfWork.SaveChanges();
    }
}