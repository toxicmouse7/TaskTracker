using MediatR;

namespace Application.TrackedTasks.Update;

public record UpdateTrackedTaskCommand(Guid TaskId, string NewContent) : IRequest;
