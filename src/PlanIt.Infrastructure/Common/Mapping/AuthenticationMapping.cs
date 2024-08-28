using Microsoft.Identity.Client;
using PlanIt.Domain.Entities;
using PlanIt.Infrastructure.Authentication;

namespace PlanIt.Infrastructure.Common.Mapping;

public static class AuthenticationMapping
{
    public static User ToUser(this ApplicationUser applicationUser) => (
        new User{
            Id = applicationUser.Id,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Email = applicationUser.Email!
        }
    );

    public static ApplicationUser ToApplicationUser(this User user) => (
        new ApplicationUser {
            UserName = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }
    );
}