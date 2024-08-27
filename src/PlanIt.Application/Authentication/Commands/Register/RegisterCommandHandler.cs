using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Domain.Entities;

namespace PlanIt.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
       // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
        }

        // 2. Create user (generate unique ID) & Persist to DB
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        // 3. Create JWT token

        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}