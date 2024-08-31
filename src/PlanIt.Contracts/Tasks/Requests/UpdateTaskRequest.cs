namespace PlanIt.Contracts.Tasks.Requests;
public record UpdateTaskRequest(
    string Name,
    string Description
);