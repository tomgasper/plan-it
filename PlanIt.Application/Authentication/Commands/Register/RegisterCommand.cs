using FluentResults;
using MediatR;
using PlanIt.Application.Services.Authentication.Common;

namespace PlanIt.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<Result<AuthenticationResult>>;