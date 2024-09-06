namespace  PlanIt.Contracts.Authenthication;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Token
);