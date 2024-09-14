using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;

namespace PlanIt.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userId = UserId.FromString(query.UserId);
        // Find user
        var result = await _userRepository.GetAsync(userId);

        if (result is null)
        {
            return Result.Fail<User>(new NotFoundError($"Couldn't find user with id: {userId.Value}"));
        }

        // Return it
        return result;
    }
}