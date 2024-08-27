using PlanIt.Domain.Entities;

namespace PlanIt.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        public Task AddAsync(User user);
    }
}