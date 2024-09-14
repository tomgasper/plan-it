using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;
using PlanIt.Domain.ProjectAggregate.ValueObjects;

namespace PlanIt.Application.Projects.Queries.GetProjectWithDetails;

public class GetProjectWithDetailsQueryHandler : IRequestHandler<GetProjectWitDetailsQuery, Result<DetailedProjectResponse>>
{
    IProjectRepository _projectRepository;

    public GetProjectWithDetailsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<DetailedProjectResponse>> Handle(GetProjectWitDetailsQuery request, CancellationToken cancellationToken)
    {
        var projectId = ProjectId.Create(new Guid(request.ProjectId));

        // Retrieve project
        var project = await _projectRepository.GetWithDetailsAsync(projectId);

        if (project is null)
        {
            return Result.Fail<DetailedProjectResponse>(new NotFoundError($"No project found with provided Project Id: {projectId.Value}"));
        }

        return Result.Ok(project);
    }
}