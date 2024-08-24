using PlanIt.Domain.Models;

namespace PlanIt.Domain.TaskComment.ValueObjects;

public sealed class TaskCommentId : ValueObject
{
    public Guid Value { get; }

    private TaskCommentId(Guid value)
    {
        Value = value;
    }

    public static TaskCommentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}