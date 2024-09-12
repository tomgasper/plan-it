using FluentResults;
using MediatR;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.Application.Workspaces.Queries.GetWorkspace;

public record GetWorkspaceQuery
(
    string WorkspaceId
) : IRequest<Result<Workspace>>;