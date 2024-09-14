using System.Reflection.Metadata.Ecma335;
using FluentResults;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanIt.Application.Common.Interfaces.Persistence;
using PlanIt.Domain.UserAggregate;
using PlanIt.Domain.UserAggregate.ValueObjects;
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
        // User Id is based on the Identity User (FK relationship)
        var appUser = user.ToApplicationUser(email);
        var createUserResult = await _userManager.CreateAsync(appUser, userPassword);

        _dbContext.DomainUsers.Add(user);

        await _dbContext.SaveChangesAsync();

        if (createUserResult.Succeeded) return user;
        else return Result.Fail<User>(createUserResult.Errors.Select(e => new IdentityError(e.Description) ));
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.DomainUsers.ToListAsync();
    }

    public async Task<User?> GetAsync(UserId userId)
    {
        // Identity and user objects are seperate
        // User Id is based on the Identity User (FK relationship)
        return await _dbContext.DomainUsers.FirstOrDefaultAsync( domainUser => domainUser.Id == userId.Value );
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

    public async Task<Result> UpdateIdentityUserAsync(
        Guid userId,
        string? email,
        string? oldPassword,
        string? newPassword)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());

        if (identityUser is null)
        {
            return Result.Fail(new NotFoundError($"Couldn't find identity user with id: {userId}"));
        }

        if (!string.IsNullOrEmpty(email))
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(identityUser, email);
            var result = await _userManager.ChangeEmailAsync(identityUser, email, token);

            if (!result.Succeeded)
            {
                return Result.Fail(new InternalServerError("Couldn't change the user's email. Please try again later."));
            }

        }

        if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
        {
            var result = await _userManager.ChangePasswordAsync(identityUser, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                return Result.Fail(new InternalServerError("Couldn't change the user's password. Please try again later."));
            }
        }

        return Result.Ok();
    }
}