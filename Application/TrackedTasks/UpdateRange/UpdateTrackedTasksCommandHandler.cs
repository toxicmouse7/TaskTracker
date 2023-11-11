using Application.TrackedTasks.Update;
using Domain.Abstractions;
using MediatR;

namespace Application.TrackedTasks.UpdateRange;

public class UpdateTrackedTasksCommandHandler : IRequestHandler<UpdateTrackedTasksCommand>
{
    private readonly ITrackedTaskRepository _trackedTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTrackedTasksCommandHandler(ITrackedTaskRepository trackedTaskRepository, IUnitOfWork unitOfWork)
    {
        _trackedTaskRepository = trackedTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTrackedTasksCommand request, CancellationToken cancellationToken)
    {
        _trackedTaskRepository.UpdateRange(request.Tasks);
        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);
        _unitOfWork.ChangeTracker.Clear();
    }
}