using PlanIt.Application.Workspaces.Commands.AssignProjectToWorkspace;
using PlanIt.Application.Workspaces.Commands.CreateWorkspace;
using PlanIt.Application.Workspaces.Commands.UpdateWorkspace;
using PlanIt.Contracts.Workspace.Requests;
using PlanIt.Contracts.Workspace.Responses;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;
using PlanIt.WebApi.Common.Mapping;

public static class WorkspaceMapping
{
    public static CreateWorkspaceCommand MapToCommand(this CreateWorkspaceRequest request) => (
        new CreateWorkspaceCommand(
            Name: request.Name,
            Description: request.Description
        )
    );

    public static UpdateWorkspaceCommand MapToCommand(this UpdateWorkspaceRequest request, string projectId) => (
        new UpdateWorkspaceCommand(
            WorkspaceId: projectId,
            Name: request.Name,
            Description: request.Description
        )
    );

    public static AssignProjectToWorkspaceCommand MapToCommand(this AssignProjectToWorkspaceRequest request, string workspaceId) => (
        new AssignProjectToWorkspaceCommand(
            ProjectId: request.ProjectId,
            WorkspaceId: workspaceId
        )
    );

    public static WorkspaceResponse MapToResponse(this Workspace workspace) => (
        new WorkspaceResponse(
            Id: workspace.Id.Value.ToString(),
            Name: workspace.Name,
            Description: workspace.Description
        )
    );

    public static WorkspaceProjectsResponse MapToResponse(this List<Project> projects, string workspaceId) => (
        new WorkspaceProjectsResponse(
            WorkspaceId: workspaceId,
            Projects: projects.Select( p => p.MapToResponse() ).ToList()
        )
    );
}