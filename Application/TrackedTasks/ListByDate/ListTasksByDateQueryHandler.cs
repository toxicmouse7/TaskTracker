using Domain.Abstractions;
using Domain.Entities.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.ListByDate;

public class ListTasksByDateQueryHandler : IRequestHandler<ListTasksByDateQuery, IEnumerable<TrackedTask>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ListTasksByDateQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<TrackedTask>> Handle(ListTasksByDateQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);
    }
}