namespace PlanIt.Contracts.Projects.Requests;

public record CreateProjectRequest(
    string Name,
    string Description,
    List<ProjectTaskRequest> ProjectTasks
);
public record ProjectTaskRequest
(
    string Name,
    string Description
);