using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;

public class AssignProjectToWorkspaceCommandHandler : IRequestHandler<AssignProjectToWorkspaceCommand, Result<Workspace>>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext;

    public AssignProjectToWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository, IUserContext userContext, IProjectRepository projectRepository)
    {
        _workspaceRepository = workspaceRepository;
        _userContext = userContext;
        _projectRepository = projectRepository;
    }

    public async Task<Result<Workspace>> Handle(AssignProjectToWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var userId = new Guid(_userContext.TryGetUserId());
        var workspaceId = WorkspaceId.Create(new Guid(command.WorkspaceId));
        var projectId = ProjectId.Create(new Guid(command.ProjectId));

        // 1. Check if workspace and project exist
        var workspace = await _workspaceRepository.GetAsync(workspaceId);
        if (workspace is null)
        {
            return Result.Fail<Workspace>(new NotFoundError($"Workspace with id: {workspaceId.Value} could not be found."));
        }

        var project = await _projectRepository.GetAsync(projectId);
        if (project is null)
        {
            return Result.Fail<Workspace>(new NotFoundError($"Project with id: {projectId.Value} could not be found."));
        }
        
        // 2. Check if the user has permission to add a project(is owner of workspace and owner of project)
        if (workspace.WorkspaceOwnerId.Value != userId || project.ProjectOwnerId.Value != userId )
        {
            return Result.Fail<Workspace>(new ForbiddenError("You don't have access to the specified workspace or project!"));
        }

        // 3. Assert that the project hadn't been added already
        if (workspace.ProjectIds.Contains(project.Id))
        {
            return Result.Fail<Workspace>( new ConflictError($"The project with id: {project.Id.Value} and name: {project.Name} is already assigned to the workspace."));
        }

        workspace.AssignProject(project.Id);

        await _workspaceRepository.SaveChangesAsync();

        return workspace;
    }
}