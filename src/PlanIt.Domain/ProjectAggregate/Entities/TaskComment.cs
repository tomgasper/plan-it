using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.TaskComment.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate.Entities;

public sealed class TaskComment : Entity<TaskCommentId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectTaskId ProjectTaskId { get; private set; }

    private TaskComment(
       TaskCommentId id,
       ProjectTaskId projectTaskId,
       string name,
       string description
    ) : base(id)
    {
        Name = name;
        Description = description;
        ProjectTaskId = projectTaskId;
    }

#pragma warning disable CS8618
    private TaskComment() { }
#pragma warning restore CS8618

    public static TaskComment Create(
       ProjectTaskId projectTaskId,
       string name,
       string description
    )
    {
        return new TaskComment(
           TaskCommentId.CreateUnique(),
           projectTaskId,
           name,
           description
        );
    }
}