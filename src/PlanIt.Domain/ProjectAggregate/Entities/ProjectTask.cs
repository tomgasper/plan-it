using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.TaskWorker.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate.Entities;

public sealed class ProjectTask : AggregateRoot<ProjectTaskId, Guid>
{
    private readonly List<TaskComment> _taskComments = new();
    private readonly List<TaskWorkerId> _taskWorkerIds;
    private ProjectTask(ProjectTaskId id,
        TaskOwnerId taskOwnerId,
        string name,
        string description,
        DateTime dueDate,
        List<TaskWorkerId> assignedUsers
        ) : base(id)
    {
        Id = id;
        TaskOwnerId = taskOwnerId;
        Name = name;
        Description = description;
        DueDate = dueDate; 
        _taskWorkerIds = assignedUsers.ToList();
    }

    #pragma warning disable CS8618
    private ProjectTask()
    {

    }
    #pragma warning restore CS8618

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate {get; private set;}
    public TaskOwnerId TaskOwnerId { get; private set; }
    public IReadOnlyList<TaskComment> TaskComments => _taskComments;
    public IReadOnlyList<TaskWorkerId> TaskWorkerIds => _taskWorkerIds;
    public static ProjectTask Create(TaskOwnerId taskOwnerId, string name, string description, DateTime dueDate, List<TaskWorkerId> assignedUsers)
    {
        return new(
            ProjectTaskId.CreateUnique(),
            taskOwnerId,
            name,
            description,
            dueDate,
            assignedUsers
        );
    }
    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void AddComment(string commentName, string commentDescription)
    {
        var comment = TaskComment.Create(Id, commentName, commentDescription);
        _taskComments.Add(comment);
    }
}