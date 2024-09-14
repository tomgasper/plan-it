using FluentResults;
using MediatR;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    string UserId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? OldPassword,
    string? NewPassword

) : IRequest<Result<User>>;