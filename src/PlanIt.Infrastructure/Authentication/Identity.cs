using FluentResults;
using Microsoft.AspNetCore.Identity;
using PlanIt.Application.Common.Interfaces.Authentication;
namespace PlanIt.Infrastructure.Authentication;

public class Identity : IIdentity
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Identity(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<Result> ValidateByEmail(string userEmail, string userPassword)
    {
        var user = new ApplicationUser {
            UserName = userEmail,
            Email = userEmail
        };

       var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

       if (signInResult.Succeeded) return Result.Ok();
       else return Result.Fail( signInResult.ToResult().Errors );
    }
}