namespace PlanIt.Contracts.Tasks.Requests;

public record AddCommentRequest(
    string Name,
    string Description
);