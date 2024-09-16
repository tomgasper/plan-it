using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Tasks.Commands.MoveToAnotherProject;

public class MoveToAnotherProjectCommandHandler : IRequestHandler<MoveToAnotherProjectCommand, Result<Project>>
{
    private readonly IUserContext _userContext;
    private readonly IProjectRepository _projectRepository;

    public MoveToAnotherProjectCommandHandler(IUserContext userContext, IProjectRepository projectRepository)
    {
        _userContext = userContext;
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> Handle(MoveToAnotherProjectCommand request, CancellationToken cancellationToken)
    {
        // Check if the user is the owner of the first and the second project
        var loggedInUser = _userContext.TryGetUserId();

        var loggedInUserId = ProjectOwnerId.FromString(loggedInUser);
        var taskId = ProjectTaskId.FromString(request.TaskId);
        var fromProjectId = ProjectId.FromString(request.ProjectId);
        var toProjectId = ProjectId.FromString(request.AnotherProjectId);

        // Retrieve the projects from repository
        var fromProject = await _projectRepository.GetAsync(fromProjectId);
        var toProject = await _projectRepository.GetAsync(toProjectId);

        if (fromProject is null)
        {
            return Result.Fail(new NotFoundError($"The project with id: {fromProjectId.Value} from which you're moving the task doesn't exit"));
        }

        if (toProject is null)
        {
            return Result.Fail(new NotFoundError($"The project with id: {toProjectId.Value} to which you're moving the task doesn't exit"));
        }

        if (fromProject.ProjectOwnerId != loggedInUserId || toProject.ProjectOwnerId != loggedInUserId)
        {
            return Result.Fail(new ForbiddenError("You must be the owner of both projects to be able to move the task."));
        }

        // Move the task to the new project
        var taskToMove = fromProject.MoveOutTask(taskId);
        if (taskToMove is null)
        {
            return Result.Fail(new NotFoundError($"The task with id:{taskId.Value} you're trying to move doesn't exist."));
        }
        toProject.CopyTask(taskToMove);

        // Persist the changes
        await _projectRepository.UpdateAsync();

        return Result.Ok(toProject);
    }
}