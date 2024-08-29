using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    void Add(Project project);

    public Task<Project?> GetAsync(string projectId);

    public Task UpdateAsync();
}