namespace PlanIt.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(Guid usedId, string firstName, string lastName);
}