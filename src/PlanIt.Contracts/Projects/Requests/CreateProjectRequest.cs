namespace PlanIt.Contracts.Projects.Requests;

public record CreateProjectRequest(
    string Name,
    string Description,
    List<ProjectTask> ProjectTasks
);
public record ProjectTask
(
    string Name,
    string Description
);