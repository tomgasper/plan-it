using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;

namespace PlanIt.Application.Workspaces.Queries.GetWorkspaceProjects.Dto;

public record WorkspaceProjectsResponse
(
    string WorkspaceId,
    List<DetailedProjectResponse> Projects
);