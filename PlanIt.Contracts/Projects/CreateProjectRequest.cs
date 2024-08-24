namespace PlanIt.Contracts.Projects;

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