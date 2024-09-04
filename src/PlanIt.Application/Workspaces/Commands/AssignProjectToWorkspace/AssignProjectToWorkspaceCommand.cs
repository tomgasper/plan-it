using FluentResults;
using MediatR;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;

public record AssignProjectToWorkspaceCommand
(
    string ProjectId,
    string WorkspaceId
) : IRequest<Result<Workspace>>;