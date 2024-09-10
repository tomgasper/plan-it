using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Queries.GetWorkspaceProjects;

public class GetWorkspaceProjectsQueryHandler : IRequestHandler<GetWorkspaceProjectsQuery, Result<List<Project>>>
{
    private readonly IProjectRepository _projectRepository;

    public GetWorkspaceProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<List<Project>>> Handle(GetWorkspaceProjectsQuery query, CancellationToken cancellationToken)
    {
        var workspaceId = WorkspaceId.FromString(query.WorkspaceId);

        // Get project associated with the workspace
        var projects = await _projectRepository.GetProjectsForWorkspaceAsync(workspaceId);

        if (projects is null)
        {
            return Result.Fail<List<Project>>(new NotFoundError($"Couldn't find project for a workspace with id: ${workspaceId.Value}"));
        }
     
        return projects;
    }
}