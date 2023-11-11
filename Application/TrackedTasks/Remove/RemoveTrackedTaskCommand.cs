using MediatR;

namespace Application.TrackedTasks.Remove;

public record RemoveTrackedTaskCommand(Guid TaskId) : IRequest;