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
    private readonly PlanItDbContext _dbContext;

    public UserRepository(UserManager<ApplicationUser> userManager, PlanItDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task<Result<User>> AddAsync(User user, string email, string userPassword)
    {
        // Identity and user objects are seperate
        // User Id is based on the Identity User
        var appUser = user.ToApplicationUser(email);
        var createUserResult = await _userManager.CreateAsync(appUser, userPassword);

        _dbContext.DomainUsers.Add(user);

        await _dbContext.SaveChangesAsync();

        if (createUserResult.Succeeded) return user;
        else return Result.Fail<User>(createUserResult.Errors.Select(e => new IdentityError(e.Description) ));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        // Get identity user
        var appUser = await _userManager.FindByEmailAsync(email);

        // Get specific user by ID
        if (appUser is not null)
        {
            var domainUser = _dbContext.DomainUsers.FirstOrDefault( domainUser => domainUser.Id == appUser.Id );
            return domainUser;
        }
        return null;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}