using PlanIt.Domain.Models;

namespace PlanIt.Domain.ProjectAggregate.ValueObjects;

public sealed class TaskOwnerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private TaskOwnerId(Guid value)
    {
        Value = value;
    }

    public static TaskOwnerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static TaskOwnerId Create(Guid id)
    {
        return new TaskOwnerId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}