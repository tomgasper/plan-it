using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Authentication;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Services.Authentication.Common;
using PlanIt.Domain.UserAggregate;

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
       var appUser = await _userRepository.GetUserByEmail(command.Email);
        if (appUser is not null)
        {
            return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
        }

        // 2. Create user (generate unique ID) & Persist to DB
        var user = User.Create(
            id: Guid.NewGuid(),
            firstName: command.FirstName,
            lastName: command.LastName
        );

        var createdUser = await _userRepository.AddAsync(user, command.Email, command.Password);

        if (createdUser.IsFailed)
        {
            return Result.Fail<AuthenticationResult>( createdUser.Errors );
        }

        await _userRepository.SaveChangesAsync();

        // 3. Create JWT token
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}