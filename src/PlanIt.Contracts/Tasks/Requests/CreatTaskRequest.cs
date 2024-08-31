namespace PlanIt.Contracts.Tasks.Requests;

public record CreateTaskRequest(
    string Name,
    string Description
);