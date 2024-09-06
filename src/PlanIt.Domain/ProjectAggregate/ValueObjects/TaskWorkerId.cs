using PlanIt.Domain.Models;

namespace PlanIt.Domain.TaskWorker.ValueObjects;

public sealed class TaskWorkerId : IdValueObject
{
    private TaskWorkerId(Guid value) : base(value) {}
    public static TaskWorkerId Create()
    {
        return new( Guid.NewGuid());
    }
}
