namespace PlanIt.Contracts.Users.Responses;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string AvatarUrl
);