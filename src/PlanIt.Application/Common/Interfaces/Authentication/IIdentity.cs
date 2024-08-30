using FluentResults;

namespace PlanIt.Application.Common.Interfaces.Authentication;

public interface IIdentity
{
    public Task<Result> ValidateByEmail(string userEmail, string userPassword);
}