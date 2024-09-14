using FluentResults;
using MediatR;

namespace PlanIt.Application.Projects.Queries.GetProjectWithDetails;

using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;

public record GetProjectWitDetailsQuery
(
    string ProjectId
) : IRequest<Result<DetailedProjectResponse>>;