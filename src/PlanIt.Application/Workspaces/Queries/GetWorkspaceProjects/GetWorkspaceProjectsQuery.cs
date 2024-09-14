using FluentResults;
using MediatR;
using PlanIt.Application.Projects.Queries.GetProjectWithDetails.Dto;
using PlanIt.Domain.ProjectAggregate;

namespace PlanIt.Application.Workspaces.Queries.GetWorkspaceProjects;

public record GetWorkspaceProjectsQuery(
    string WorkspaceId
) : IRequest<Result<List<DetailedProjectResponse>>>;