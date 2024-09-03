using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Authentication.Commands.Queries.Login;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Domain.UserAggregate;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IIdentity _identity;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository, IIdentity identity)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
        _identity = identity;
    }


    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // 1. Check the user exists
        // 2. Validate the password is correct
        

        if (await _userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Result.Fail<AuthenticationResult>(
                new InvalidCredentialsError("Invalid credentials. The combination of the provided email and password is not valid!"));
        }
        
        var validationResult = await _identity.ValidateByEmail(query.Email, query.Password);

        if ( validationResult.IsFailed )
        {
            return Result.Fail<AuthenticationResult>(
                new InvalidCredentialsError("Invalid credentials. The combination of the provided email and password is not valid!"));
        }

        ;

        // 1. Check the user exists
        /*
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Result.Fail<AuthenticationResult>(
                new InvalidCredentialsError("Invalid credentials. The combination of the provided email and password is not valid!"));
        }

        // 2. Validate the password is correct
        if (user.Password != query.Password)
        {
            return Result.Fail<AuthenticationResult>(
                new InvalidCredentialsError("Invalid credentials. The combination of the provided email and password is not valid!"));
        }
        */

        var token = _jwtGenerator.GenerateToken(user);

        // 3. Create JWT
        return new AuthenticationResult(
            user,
            token
        );
    }
}