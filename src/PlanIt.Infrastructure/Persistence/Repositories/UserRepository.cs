using FluentResults;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate;
using PlanIt.Infrastructure.Authentication;
using PlanIt.Infrastructure.Common.Mapping;

namespace PlanIt.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<User>> AddAsync(User user, string userPassword)
    {
        var appUser = user.ToApplicationUser();
        var createUserResult = await _userManager.CreateAsync(appUser, userPassword);

        if (createUserResult.Succeeded) return appUser.ToUser();
        else return Result.Fail<User>(createUserResult.Errors.Select(e => new IdentityError(e.Description) ));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);

        if (appUser is not null) return appUser.ToUser();
        else return null;
    }
}