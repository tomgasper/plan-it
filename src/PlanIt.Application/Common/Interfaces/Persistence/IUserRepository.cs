using FluentResults;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        public Task<Result<User>> AddAsync(User user, string email, string password);
        public Task<User?> GetUserByEmail(string email);
        public Task SaveChangesAsync();
    }
}