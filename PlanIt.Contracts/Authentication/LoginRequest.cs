namespace  PlanIt.Contracts.Authenthication;

public record LoginRequest(
    string Email,
    string Password
);