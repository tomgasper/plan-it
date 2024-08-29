using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

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

    public async Task<Project?> GetAsync(string projectId)
    {
        // Converting to Id stored as Value Object
        if (!Guid.TryParse(projectId, out Guid projectIdGuid)) return null;
        var projectIdObj = ProjectId.Create(projectIdGuid);
        
        return await _dbContext.Projects.FirstOrDefaultAsync( p => p.Id == projectIdObj  );
    }

    public async Task UpdateAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Project project)
    {
        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();
    }
}