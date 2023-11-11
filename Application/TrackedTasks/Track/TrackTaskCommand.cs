using Domain.ReactiveEntities;
using MediatR;

namespace Application.TrackedTasks.Track;

public record TrackTaskCommand(ReactiveTask ReactiveTask, CancellationToken CancellationToken) : IRequest;