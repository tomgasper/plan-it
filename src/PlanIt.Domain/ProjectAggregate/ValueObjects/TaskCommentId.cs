using PlanIt.Domain.Models;

namespace PlanIt.Domain.TaskComment.ValueObjects;

public sealed class TaskCommentId : IdValueObject
{
	private TaskCommentId(Guid value) : base(value) {}

    public static TaskCommentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static TaskCommentId Create(Guid value)
    {
        return new(value);
    }
}