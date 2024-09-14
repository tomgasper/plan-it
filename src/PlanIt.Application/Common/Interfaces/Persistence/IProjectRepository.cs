using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;

namespace PlanIt.Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    Task AddAsync(Project project);

    public Task<Project?> GetAsync(ProjectId projectId);
    public Task<DetailedProjectResponse?> GetWithDetailsAsync(ProjectId projectId);
    public Task<List<DetailedProjectResponse>> GetProjectsForWorkspaceAsync(WorkspaceId workspaceId);
    public Task UpdateAsync();
    public Task DeleteAsync(Project project);
}