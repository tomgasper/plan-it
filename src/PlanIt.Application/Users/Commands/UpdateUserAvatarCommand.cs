using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Application.Users.Commands;

public record UpdateUserAvatarCommand(
    string UserId,
    IFormFile Avatar
) : IRequest<Result<User>>;