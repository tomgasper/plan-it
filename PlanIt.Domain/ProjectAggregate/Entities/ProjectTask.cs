using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.ProjectWorker.ValueObjects;
using PlanIt.Domain.TaskComment.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate.Entities;

public sealed class ProjectTask : Entity<ProjectTaskId>
{
    private readonly List<TaskCommentId> _taskComments = new();
    private readonly List<ProjectWorkerId> _projectWorkers = new();
    private ProjectTask(ProjectTaskId id, TaskOwnerId taskOwnerId, string name, string description) : base(id)
    {
        Id = id;
        TaskOwnerId = taskOwnerId;
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public TaskOwnerId TaskOwnerId { get; }
    public IReadOnlyList<TaskCommentId> TaskCommentIds => _taskComments;
    public IReadOnlyList<ProjectWorkerId> ProjectWorkerIds => _projectWorkers;

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