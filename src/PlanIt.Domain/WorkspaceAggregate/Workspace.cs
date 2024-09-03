using PlanIt.Domain.Models;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Domain.WorkspaceAggregate;

public sealed class Workspace : AggregateRoot<WorkspaceId, Guid>
{
    private Workspace(
        WorkspaceId id,
        string name,
        string description,
        WorkspaceOwnerId workspaceOwnerId,
        DateTime createdDateTime,
        DateTime updateDateTime ) : base(id)
        {
            Name = name;
            Description = description;
            WorkspaceOwnerId = workspaceOwnerId;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updateDateTime;
        }

    private readonly List<ProjectId> _projectIds = [];

    public string Name {get; private set;}
    public string Description {get; private set;}
    public WorkspaceOwnerId WorkspaceOwnerId { get; private set;}
    public DateTime CreatedDateTime { get; private set;}
    public DateTime UpdatedDateTime {get; private set;}
    public IReadOnlyList<ProjectId> ProjectIds => _projectIds;

    public static Workspace Create(
        string name,
        string description,
        WorkspaceOwnerId workspaceOwnerId
        )
        {
            var workspace = new Workspace(
                WorkspaceId.CreateUnique(),
                name,
                description,
                workspaceOwnerId,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            // Add domain event

            return workspace;
        }

}