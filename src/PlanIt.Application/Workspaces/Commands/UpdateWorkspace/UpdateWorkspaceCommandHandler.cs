using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Commands.UpdateWorkspace;

public class UpdateWorkspaceCommandHandler : IRequestHandler<UpdateWorkspaceCommand, Result<Workspace>>
{
    private readonly IUserContext _userContext;
    private readonly IWorkspaceRepository _workspaceRepository;

    public UpdateWorkspaceCommandHandler(IUserContext userContext, IWorkspaceRepository workspaceRepository)
    {
        _userContext = userContext;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Result<Workspace>> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        // Convert strings to proper Id objects
        var userId = UserId.FromString(_userContext.TryGetUserId());
        var workspaceId = WorkspaceId.FromString(request.WorkspaceId);

        // Get user workspace
        var workspace = await _workspaceRepository.GetAsync(workspaceId);
        if (workspace is null)
        {
            return Result.Fail<Workspace>(new NotFoundError($"Couldn't find the workspace with id: {workspaceId.Value}"));
        }

        // Check if the user has permission to access the resource
        if (workspace.WorkspaceOwnerId.Value != userId.Value)
        {
            return Result.Fail<Workspace>(new ForbiddenError($"You don't have permission to modify this workspace."));
        }

        // Update the name and description of the workspace
        workspace.ChangeName(request.Name);
        workspace.ChangeDescription(request.Description);

        // Persist the changes
        await _workspaceRepository.SaveChangesAsync();

        // Return the object
        return workspace;
    }
}