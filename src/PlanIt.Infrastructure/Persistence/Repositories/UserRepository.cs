using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.Entities;

namespace PlanIt.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public async Task AddAsync(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}