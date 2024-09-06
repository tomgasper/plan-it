using PlanIt.Domain.Models;

namespace PlanIt.Domain.WorkspaceAggregate.ValueObjects;

public sealed class WorkspaceId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private WorkspaceId(Guid value)
    {
        Value = value;
    }

    public static WorkspaceId CreateUnique()
    {
        return new WorkspaceId(Guid.NewGuid());
    }

    public static WorkspaceId Create(Guid value)
    {
        return new WorkspaceId(value);
    }

    public static WorkspaceId FromString(string id)
    {
        return new WorkspaceId(new Guid(id));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}