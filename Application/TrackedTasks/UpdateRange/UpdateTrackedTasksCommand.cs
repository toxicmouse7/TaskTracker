using Domain.Entities.Tasks;
using MediatR;

namespace Application.TrackedTasks.UpdateRange;

public record UpdateTrackedTasksCommand(IEnumerable<TrackedTask> Tasks) : IRequest;