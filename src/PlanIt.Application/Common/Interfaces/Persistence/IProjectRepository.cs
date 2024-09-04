using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    void Add(Project project);

    public Task<Project?> GetAsync(ProjectId projectId);

    public Task UpdateAsync();

    public Task DeleteAsync(Project project);
}