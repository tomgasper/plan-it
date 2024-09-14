using FluentResults;
using Microsoft.AspNetCore.Identity;
using PlanIt.Application.Common.Interfaces.Authentication;
namespace PlanIt.Infrastructure.Authentication;

public class Identity : IIdentity
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public Identity(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> ValidateByEmail(string userEmail, string userPassword)
    {
       var user = await _userManager.FindByEmailAsync(userEmail);

       if (user is null) return Result.Fail( new NotFoundError($"Couldn't find user with email: {userEmail}"));

       var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);


       if (signInResult.Succeeded) { return Result.Ok(); }
       else { 
        return Result.Fail( new InvalidCredentialsError("Couldn't log in the user. Check the input data.") );
       }
    }
}