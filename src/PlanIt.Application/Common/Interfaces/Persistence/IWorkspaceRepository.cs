using FluentResults;
using PlanIt.Domain.WorkspaceAggregate;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Common.Interfaces.Persistence;

public interface IWorkspaceRepository
{
    public Task AddAsync(Workspace workspace);
    public Task<Workspace?> GetAsync(WorkspaceId workspaceId);
    public Task SaveChangesAsync();
}