using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.ProjectAggregate;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlanIt.Domain.ProjectAggregate.ValueObjects;
using PlanIt.Domain.WorkspaceAggregate.ValueObjects;
using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;
using PlanIt.Domain.TaskWorker.ValueObjects;
using System.Reflection.Metadata.Ecma335;

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
        return await _dbContext.Projects.Include(p => p.ProjectTasks)
                                            .ThenInclude(pt => pt.TaskWorkerIds)
                                        .FirstOrDefaultAsync( p => p.Id == projectId  );
    }

    public async Task<DetailedProjectResponse?> GetWithDetailsAsync(ProjectId projectId)
    {
        var project = await _dbContext.Projects.Include(p => p.ProjectTasks)
                                               .ThenInclude(pt => pt.TaskComments)
                                               .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project is null) return null;

        var taskWorkerIds = project.ProjectTasks.SelectMany( pt => pt.TaskWorkerIds ).Distinct().Select(tw => tw.Value.ToString()).ToList();

        var userDict = await _dbContext.DomainUsers
                                    .Where(u => taskWorkerIds.Contains(u.Id.ToString()))
                                    .ToDictionaryAsync(u => u.Id.ToString(), u => new TaskWorkerResponse(
                                        u.Id.ToString(),
                                        u.FirstName,
                                        u.LastName,
                                        u.AvatarUrl
                                    ));
    
    var detailedTasks = project.ProjectTasks.Select(pt => new DetailedProjectTaskResponse(
        pt.Id.Value,
        pt.Name,
        pt.Description,
        pt.TaskOwnerId.Value.ToString(),
        pt.DueDate,
        pt.TaskComments.Select(tc => new DetailedTaskCommentResponse(
            tc.Name,
            tc.Description
        )).ToList(),
        pt.TaskWorkerIds
            .Select(twId => userDict.GetValueOrDefault(twId.Value.ToString()))
            .Where(tw => tw != null)
            .ToList()!
    )).ToList();

    return new DetailedProjectResponse(
        project.Id.Value,
        project.Name,
        project.Description,
        detailedTasks,
        project.ProjectOwnerId.Value.ToString(),
        project.CreatedDateTime,
        project.UpdatedDateTime
    );

    }

    public async Task<List<DetailedProjectResponse>> GetProjectsForWorkspaceAsync(WorkspaceId workspaceId)
{
    var projects = await _dbContext.Projects.Where(p => p.WorkspaceId == workspaceId).ToListAsync();

    var detailedProjects = new List<DetailedProjectResponse>();

    foreach (var project in projects)
    {
        var detailedProject = await GetWithDetailsAsync(project.Id);
        if (detailedProject != null)
        {
            detailedProjects.Add(detailedProject);
        }
    }

    return detailedProjects;
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