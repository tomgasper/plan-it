using PlanIt.Domain.Entities;

namespace PlanIt.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}