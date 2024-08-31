namespace PlanIt.Contracts.Projects.Requests;

public record UpdateProjectRequest(
    string Name,
    string Description
);