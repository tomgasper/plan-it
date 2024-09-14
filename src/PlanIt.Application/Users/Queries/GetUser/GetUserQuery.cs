using FluentResults;
using MediatR;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Users.Queries.GetUser;

public record GetUserQuery(
    string UserId
) : IRequest<Result<User>>;