using PlanIt.Domain.Entities;

namespace PlanIt.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);