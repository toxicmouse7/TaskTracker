using Domain.Entities.Tasks;
using MediatR;

namespace Application.TrackedTasks.Create;

public record CreateTrackedTaskCommand(TrackedTask TrackedTask) : IRequest;
