using PlanIt.Domain.Models;

namespace PlanIt.Domain.TaskWorker.ValueObjects;

public sealed class TaskWorkerId : ValueObject
{
    public Guid Value {get;}

    private TaskWorkerId(Guid value ) { Value = value; }

    public static TaskWorkerId Create()
    {
        return new( Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
