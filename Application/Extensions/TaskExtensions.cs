using Domain.Entities.Tasks;
using Domain.ReactiveEntities;

namespace Application.Extensions;

public static class TaskExtensions
{
    public static TrackedTask ToTask(this ReactiveTask reactiveTask)
    {
        return new TrackedTask(reactiveTask.Id, reactiveTask.Content, reactiveTask.TimeWasted, reactiveTask.CreatedOn);
    }

    public static ReactiveTask ToReactiveTask(this TrackedTask trackedTask)
    {
        return new ReactiveTask(trackedTask.Id, trackedTask.Content, trackedTask.TimeWasted, trackedTask.CreatedOn);
    }
}