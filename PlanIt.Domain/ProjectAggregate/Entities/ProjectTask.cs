using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.ProjectWorker.ValueObjects;
using PlanIt.Domain.TaskComment.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate.Entities;

public sealed class ProjectTask : Entity<ProjectTaskId>
{
    private readonly List<TaskCommentId> _taskComments;
    private readonly List<ProjectWorkerId> _projectWorkers;
    private ProjectTask(ProjectTaskId id, string name, string description) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public TaskOwnerId TaskOwnerId { get; }
    public IReadOnlyList<TaskCommentId> TaskCommentIds => _taskComments;
    public IReadOnlyList<ProjectWorkerId> ProjectWorkerIds => _projectWorkers;

    public static ProjectTask Create(string name, string description)
    {
        return new(
            ProjectTaskId.CreateUnique(),
            name,
            description
        );
    }
}