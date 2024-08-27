using FluentResults;
using MediatR;
using PlanIt.Application.Services.Authentication.Common;

namespace PlanIt.Application.Authentication.Commands.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<Result<AuthenticationResult>>;