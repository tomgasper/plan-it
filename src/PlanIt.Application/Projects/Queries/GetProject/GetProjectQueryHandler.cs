using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Projects.Queries.GetProject;
using PlanIt.Domain.ProjectAggregate;
using PlanIt.Domain.ProjectAggregate.Entities;

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
        // Retrieve project
        var project = await _projectRepository.GetAsync(request.ProjectId);

        if (project is null)
        {
            return Result.Fail<Project>(new NotFoundError($"No project found with provided Project Id: {request.ProjectId}"));
        }

        return Result.Ok(project);
    }
}