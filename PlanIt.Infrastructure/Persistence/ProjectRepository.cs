using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Infrastructure.Persistence;

public class ProjectRepository : IProjectRepository
{
    private static readonly List<Project> _projects = new();
    public void Add(Project project)
    {
        _projects.Add(project);
    }
}