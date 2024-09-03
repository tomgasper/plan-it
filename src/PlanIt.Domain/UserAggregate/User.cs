using PlanIt.Domain.Models;
using PlanIt.Domain.UserAggregate.ValueObjects;

namespace PlanIt.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId, Guid>
    {
        public string FirstName { get; protected set; } = null!;
        public string LastName { get; protected set; } = null!;
        public string Email { get; protected set; } = null!;
    }
}