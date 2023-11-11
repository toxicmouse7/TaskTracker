using Application.TrackedTasks.Create;
using Domain.Abstractions;
using MediatR;

namespace Application.Tasks.Create;

public class CreateTaskCommandHandler : IRequestHandler<CreateTrackedTaskCommand>
{
    private readonly ITrackedTaskRepository _trackedTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(ITrackedTaskRepository trackedTaskRepository, IUnitOfWork unitOfWork)
    {
        _trackedTaskRepository = trackedTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateTrackedTaskCommand request, CancellationToken cancellationToken)
    {
        await _trackedTaskRepository.AddTaskAsync(request.TrackedTask, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _unitOfWork.ChangeTracker.Clear();
    }
}