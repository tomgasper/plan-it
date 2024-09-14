using PlanIt.Domain.Models;

namespace PlanIt.Domain.TaskWorker.ValueObjects;

public sealed class TaskWorkerId : IdValueObject
{
    private TaskWorkerId(Guid value) : base(value) {}
    public static TaskWorkerId Create()
    {
        return new( Guid.NewGuid());
    }


    public static TaskWorkerId FromString(string id)
    {
        return new TaskWorkerId( new Guid(id) );
    }
}
