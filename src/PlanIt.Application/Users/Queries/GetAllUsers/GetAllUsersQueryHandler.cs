using FluentResults;
using MediatR;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Application.Users.Queries.GetAllUsers;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;

namespace PlanIt.Application.Users.Queries.GetUser;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<User>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return users;
    }
}