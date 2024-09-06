using FluentResults;
using MediatR;
using PlanIt.Domain.WorkspaceAggregate;

namespace PlanIt.Application.Workspaces.Commands.CreateWorkspace;

public record CreateWorkspaceCommand
(
    string Name,
    string Description
) : IRequest<Result<Workspace>>;