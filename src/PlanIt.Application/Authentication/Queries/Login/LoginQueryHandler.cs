using FluentResults;
using MediatR;
using PlanIt.Application.Authentication.Commands.Queries.Login;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Domain.Entities;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }


    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // 1. Check the user exists
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

        var token = _jwtGenerator.GenerateToken(user);

        // 3. Create JWT
        return new AuthenticationResult(
            user,
            token
        );
    }
}