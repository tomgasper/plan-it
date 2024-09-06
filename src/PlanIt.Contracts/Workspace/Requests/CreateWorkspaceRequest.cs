namespace PlanIt.Contracts.Workspace.Requests;
public record CreateWorkspaceRequest
(
    string Name,
    string Description
);