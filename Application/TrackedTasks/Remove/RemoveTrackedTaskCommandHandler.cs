using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrackedTasks.Remove;

public class RemoveTrackedTaskCommandHandler : IRequestHandler<RemoveTrackedTaskCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITrackedTaskRepository _trackedTaskRepository;

    public RemoveTrackedTaskCommandHandler(IApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork, ITrackedTaskRepository trackedTaskRepository)
    {
        _applicationDbContext = applicationDbContext;
        _unitOfWork = unitOfWork;
        _trackedTaskRepository = trackedTaskRepository;
    }

    public async Task Handle(RemoveTrackedTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _applicationDbContext.Tasks.FirstOrDefaultAsync(
            t => t.Id == request.TaskId);
        if (task is null)
            return;
        
        _trackedTaskRepository.Remove(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        _unitOfWork.ChangeTracker.Clear();
    }
}