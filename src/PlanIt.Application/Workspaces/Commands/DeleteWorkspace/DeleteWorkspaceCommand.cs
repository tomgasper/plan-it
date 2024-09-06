using FluentResults;
using MediatR;

namespace PlanIt.Application.Workspaces.Commands.DeleteWorkspace;
public record DeleteWorkspaceCommand
(
    string WorkspaceId
) : IRequest<Result>;