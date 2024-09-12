using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly PlanItDbContext _dbContext;

    public ProjectRepository(PlanItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Project project)
    {
        _dbContext.Add(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Project?> GetAsync(ProjectId projectId)
    {
        return await _dbContext.Projects.FirstOrDefaultAsync( p => p.Id == projectId  );
    }

    public async Task<List<Project>> GetProjectsForWorkspaceAsync(WorkspaceId workspaceId)
    {
        return await _dbContext.Projects.Where( p => p.WorkspaceId == workspaceId).ToListAsync();
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