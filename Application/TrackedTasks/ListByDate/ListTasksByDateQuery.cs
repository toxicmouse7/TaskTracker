using Domain.Entities.Tasks;
using MediatR;

namespace Application.Tasks.ListByDate;

public record ListTasksByDateQuery(DateTime TaskDate) : IRequest<IEnumerable<TrackedTask>>;