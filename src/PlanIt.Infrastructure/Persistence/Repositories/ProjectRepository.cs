using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PlanItDbContext _dbContext;

    public ProjectRepository(PlanItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Project project)
    {
        _dbContext.Add(project);
        _dbContext.SaveChanges();
    }
}