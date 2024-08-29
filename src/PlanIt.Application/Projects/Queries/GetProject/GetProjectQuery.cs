using FluentResults;
using MediatR;

using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Projects.Queries.GetProject;

public record GetProjectQuery 
(
    string ProjectId
) : IRequest<Result<Project>>;