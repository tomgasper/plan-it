using FluentResults;
using MediatR;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.Application.Workspaces.Commands.UpdateWorkspace;

public record UpdateWorkspaceCommand(
    string WorkspaceId,
    string Name,
    string Description
) : IRequest<Result<Workspace>>;