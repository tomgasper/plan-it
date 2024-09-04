using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.TaskWorker.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate.Entities;

public sealed class ProjectTask : AggregateRoot<ProjectTaskId, Guid>
{
    private readonly List<TaskComment> _taskComments = new();
    private readonly List<TaskWorkerId> _taskWorkerIds = new();
    private ProjectTask(ProjectTaskId id, TaskOwnerId taskOwnerId, string name, string description) : base(id)
    {
        Id = id;
        TaskOwnerId = taskOwnerId;
        Name = name;
        Description = description;
    }

    #pragma warning disable CS8618
    private ProjectTask()
    {

    }
    #pragma warning restore CS8618

    public string Name { get; private set; }
    public string Description { get; private set; }

    public TaskOwnerId TaskOwnerId { get; private set; }
    public IReadOnlyList<TaskComment> TaskComments => _taskComments;
    public IReadOnlyList<TaskWorkerId> TaskWorkerIds => _taskWorkerIds;

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public static ProjectTask Create(TaskOwnerId taskOwnerId, string name, string description)
    {
        return new(
            ProjectTaskId.CreateUnique(),
            taskOwnerId,
            name,
            description
        );
    }
}