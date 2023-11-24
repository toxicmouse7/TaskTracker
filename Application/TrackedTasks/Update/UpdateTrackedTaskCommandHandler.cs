using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrackedTasks.Update;

public class UpdateTrackedTaskCommandHandler : IRequestHandler<UpdateTrackedTaskCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ITrackedTaskRepository _trackedTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTrackedTaskCommandHandler(ITrackedTaskRepository trackedTaskRepository, IUnitOfWork unitOfWork, IApplicationDbContext applicationDbContext)
    {
        _trackedTaskRepository = trackedTaskRepository;
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
    }

    public async Task Handle(UpdateTrackedTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _applicationDbContext.Tasks.FirstOrDefaultAsync(
            t => t.Id == request.TaskId,
            cancellationToken: cancellationToken);
        if (task is null)
            return;

        task.Content = request.NewContent;
        task.TimeWasted = request.NewTimeWasted;
        _trackedTaskRepository.Update(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        _unitOfWork.ChangeTracker.Clear();
    }
}