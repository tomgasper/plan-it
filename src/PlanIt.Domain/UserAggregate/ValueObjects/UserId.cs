using PlanIt.Domain.Models;

namespace PlanIt.Domain.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}