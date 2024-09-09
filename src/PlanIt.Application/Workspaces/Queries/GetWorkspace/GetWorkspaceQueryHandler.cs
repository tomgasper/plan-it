using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Workspaces.Queries.GetWorkspace;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, Result<Workspace>>
{
    private readonly IWorkspaceRepository _workspaceRepository;

    public GetWorkspaceQueryHandler(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Result<Workspace>> Handle(GetWorkspaceQuery query, CancellationToken cancellationToken)
    {
        var workspaceId = WorkspaceId.FromString(query.WorkspaceId);

        // Get workspace
        var workspace = await _workspaceRepository.GetAsync(workspaceId);

        if (workspace is null)
        {
            return Result.Fail<Workspace>(new NotFoundError($"Couldn't find a workspace with id: ${workspaceId.Value}"));
        }
        
        // Return it
        return workspace;
    }
}