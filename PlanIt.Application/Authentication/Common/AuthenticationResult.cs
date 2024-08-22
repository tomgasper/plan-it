using PlanIt.Domain.Entities;

namespace PlanIt.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);