using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);