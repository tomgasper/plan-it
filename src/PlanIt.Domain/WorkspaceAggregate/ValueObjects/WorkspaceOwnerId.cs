using PlanIt.Domain.Models;

namespace PlanIt.Domain.WorkspaceAggregate.ValueObjects;

public sealed class WorkspaceOwnerId : IdValueObject
{
    private WorkspaceOwnerId(Guid value) : base(value) {}

    public static WorkspaceOwnerId CreateUnique()
    {
        return new WorkspaceOwnerId(Guid.NewGuid());
    }

    public static WorkspaceOwnerId Create(Guid value)
    {
        return new WorkspaceOwnerId(value);
    }

     public static WorkspaceOwnerId FromString(string id)
    {
        return new WorkspaceOwnerId(new Guid(id));
    }
}