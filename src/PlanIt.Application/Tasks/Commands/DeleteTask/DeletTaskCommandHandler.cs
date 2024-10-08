using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Tasks.Commands.DeleteTask;

public class DeletTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
{
    IUserContext _userContext;
    IProjectRepository _projectRepository;

    public DeletTaskCommandHandler(IUserContext userContext, IProjectRepository projectRepository)
    {
        _userContext = userContext;
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.TryGetUserId();
        var taskId = ProjectTaskId.Create(new Guid(request.TaskId));
        var projectId = ProjectId.Create(new Guid(request.ProjectId));

        Project? project = await _projectRepository.GetAsync(projectId);

        if (project is null)
        {
            return Result.Fail(new NotFoundError($"The project with id: {projectId.Value} couldn't be found."));
        }

        project.DeleteTask(taskId, userId);

        await _projectRepository.UpdateAsync();

        return Result.Ok();
    }
}