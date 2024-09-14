using FluentResults;
using MediatR;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(
) : IRequest<Result<List<User>>>;