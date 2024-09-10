using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Infrastructure.Persistence.Repositories;

public class WorkspaceRepository : IWorkspaceRepository
{
    private readonly PlanItDbContext _dbContext;

    public WorkspaceRepository(PlanItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Workspace workspace)
    {
        _dbContext.Workspaces.Add(workspace);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Workspace workspace)
    {
        _dbContext.Workspaces.Remove(workspace);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Workspace?> GetAsync(WorkspaceId workspaceId)
    {
        return await _dbContext.Workspaces.FirstOrDefaultAsync( w => w.Id == workspaceId);
    }

    public async Task<List<Workspace>> GetUserWorkspacesAsync(WorkspaceOwnerId userId)
    {
        return await _dbContext.Workspaces.Where( w => w.WorkspaceOwnerId == userId ).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}