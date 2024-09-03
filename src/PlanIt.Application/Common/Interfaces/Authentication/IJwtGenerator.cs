using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}