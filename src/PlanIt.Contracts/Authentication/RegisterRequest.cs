namespace  PlanIt.Contracts.Authenthication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);