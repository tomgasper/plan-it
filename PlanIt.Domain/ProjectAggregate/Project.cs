using PlanIt.Domain.Models;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate;

public sealed class Project : AggregateRoot<ProjectId>
{
    private Project(
        ProjectId id,
        string name,
        string description,
        ProjectOwnerId projectOwnerId,
        List<ProjectTask> projectTasks,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        Name = name;
        Description = description;
        _projectTasks = projectTasks;
        ProjectOwnerId = projectOwnerId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private readonly List<ProjectTask> _projectTasks;
    public string Name { get; }
    public string Description { get; }
    public ProjectOwnerId ProjectOwnerId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public static Project Create(string name,
        string description,
        ProjectOwnerId projectOwnerId,
        List<ProjectTask> projectTasks)
    {
        return new(
            ProjectId.CreateUnique(),
            name,
            description,
            projectOwnerId,
            projectTasks,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

    public IReadOnlyList<ProjectTask> ProjectTasks => _projectTasks.AsReadOnly();

}