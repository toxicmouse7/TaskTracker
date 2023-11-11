using Domain.Abstractions;
using Domain.Entities.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrackedTasks.Get;

public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TrackedTask?>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetTaskQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<TrackedTask?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(
                t => t.Id == request.TaskId,
                cancellationToken: cancellationToken);
    }
}