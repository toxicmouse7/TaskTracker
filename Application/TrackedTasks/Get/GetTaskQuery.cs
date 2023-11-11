using Domain.Entities.Tasks;
using MediatR;

namespace Application.TrackedTasks.Get;

public record GetTaskQuery(Guid TaskId) : IRequest<TrackedTask?>;
