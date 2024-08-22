using MediatR;
using PlanIt.Application.Authentication.Commands.Queries.Login;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Domain.Entities;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }


    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // 1. Check the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new Exception("User with given email does not exist.");
        }

        // 2. Validate the password is correct
        if (user.Password != query.Password)
        {
            throw new Exception("Invalid password!");
        }

        var token = _jwtGenerator.GenerateToken(user);

        // 3. Create JWT
        return new AuthenticationResult(
            user,
            token
        );
    }
}