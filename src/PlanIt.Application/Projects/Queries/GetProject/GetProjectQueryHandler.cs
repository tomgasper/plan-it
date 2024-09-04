using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Projects.Queries.GetProject;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Projects.Queries;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, Result<Project>>
{
    IProjectRepository _projectRepository;

    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var projectId = ProjectId.Create(new Guid(request.ProjectId));
        
        // Retrieve project
        var project = await _projectRepository.GetAsync(projectId);

        if (project is null)
        {
            return Result.Fail<Project>(new NotFoundError($"No project found with provided Project Id: {projectId.Value}"));
        }

        return Result.Ok(project);
    }
}