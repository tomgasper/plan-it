using FluentResults;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmail(string email);
        public Task<Result<User>> AddAsync(User user, string password);
    }
}