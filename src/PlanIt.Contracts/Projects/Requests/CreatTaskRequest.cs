namespace PlanIt.Contracts.Projects.Requests;

public record CreateTaskRequest(
    string Name,
    string Description
);