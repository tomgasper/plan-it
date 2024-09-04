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
    public IReadOnlyList<ProjectTask> ProjectTasks => _projectTasks.AsReadOnly();

    public void ChangeName(string newName)
    {
        Name = newName;
        UpdatedDateTime = DateTime.Now;
    }

    public void ChangeDescription(string newDescription)
    {
        Description = newDescription;
        UpdatedDateTime = DateTime.Now;
    }

    public ProjectTask? ChangeTaskNameDescription(
        ProjectTaskId taskId,
        string userId,
        string newName,
        string newDescription)
    {
        var task = ProjectTasks.FirstOrDefault(t => t.Id == taskId);
        
        if (task is null || !IsUserAllowedToEditTask(userId, task))
        {
            return null;
        }

        task.ChangeName(newName);
        task.ChangeDescription(newDescription);
        UpdatedDateTime = DateTime.UtcNow;

        return task;
    }

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

    public ProjectTask AddNewTask(string name, string description)
    {
        ProjectTask? newTask = ProjectTask.Create(
                taskOwnerId: TaskOwnerId.Create(ProjectOwnerId.Value),
                name: name,
                description: description);

        _projectTasks.Add(newTask);

        return newTask;
    }

    public void DeleteTask(ProjectTaskId taskId, string userId)
    {
        ProjectTask? task = ProjectTasks.FirstOrDefault(t => t.Id == taskId);
        
        if (task is null || !IsUserAllowedToDeleteTask(userId, task))
        {
            return;
        }

        _projectTasks.Remove(task);
    }

    private bool IsUserAllowedToEditTask(string userId, ProjectTask task)
    {
        return ProjectOwnerId.Value.ToString() == userId || task.TaskOwnerId.Value.ToString() == userId;
    }

    private bool IsUserAllowedToDeleteTask(string userId, ProjectTask task)
    {
        return ProjectOwnerId.Value.ToString() == userId || task.TaskOwnerId.Value.ToString() == userId;
    }
}