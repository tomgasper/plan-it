using PlanIt.Domain.Models;
using PlanIt.Domain.Project.ValueObjects;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.Events;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Domain.ProjectAggregate;

public sealed class Project : AggregateRoot<ProjectId, Guid>
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

    #pragma warning disable CS8618
    private Project()
    {

    }
    #pragma warning restore CS8618

    private readonly List<ProjectTask> _projectTasks;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectOwnerId ProjectOwnerId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static Project Create(string name,
        string description,
        ProjectOwnerId projectOwnerId,
        List<ProjectTask> projectTasks)
    {
        var project = new Project(
            ProjectId.CreateUnique(),
            name,
            description,
            projectOwnerId,
            projectTasks,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        project.AddDomainEvent(new ProjectCreated(project));

        return project;
    }

    public IReadOnlyList<ProjectTask> ProjectTasks => _projectTasks.AsReadOnly();

}