using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.ValueObjects;

public sealed class TaskOwnerId : ValueObject
{
    public Guid Value { get; }

    private TaskOwnerId(Guid value)
    {
        Value = value;
    }

    public static TaskOwnerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static TaskOwnerId Create(string Id)
    {
        return new(Guid.Parse(Id));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}