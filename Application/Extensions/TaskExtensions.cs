using Domain.ReactiveEntities;
using Task = Domain.Entities.Tasks.Task;

namespace Application.Extensions;

public static class TaskExtensions
{
    public static Task ToTask(this ReactiveTask reactiveTask)
    {
        return new Task(reactiveTask.Id, reactiveTask.Content, reactiveTask.TimeWasted);
    }

    public static ReactiveTask ToReactiveTask(this Task task)
    {
        return new ReactiveTask(task.Id, task.Content, task.TimeWasted);
    }
}