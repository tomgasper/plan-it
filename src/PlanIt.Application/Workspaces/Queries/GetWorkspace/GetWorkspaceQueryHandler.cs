using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Workspaces.Queries.GetWorkspace;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, Result<Workspace>>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IUserContext _userContext;

    public GetWorkspaceQueryHandler(IWorkspaceRepository workspaceRepository, IUserContext userContext)
    {
        _workspaceRepository = workspaceRepository;
        _userContext = userContext;
    }

    public async Task<Result<Workspace>> Handle(GetWorkspaceQuery query, CancellationToken cancellationToken)
    {
        var loggedInUser = _userContext.TryGetUserId();

        var loggedInUserId = WorkspaceOwnerId.FromString(loggedInUser);
        var workspaceId = WorkspaceId.FromString(query.WorkspaceId);

        // Get workspace
        var workspace = await _workspaceRepository.GetAsync(workspaceId);

        if (workspace is null)
        {
            return Result.Fail<Workspace>(new NotFoundError($"Couldn't find a workspace with id: ${workspaceId.Value}"));
        }

        // User that is sending the request doesn't have permission to view this workspace
        if (loggedInUserId != workspace.WorkspaceOwnerId)
        {
            return Result.Fail(new ForbiddenError("You don't have permission to view this workspace."));
        }
        
        // Return it
        return workspace;
    }
}