using PlanIt.Domain.UserAggregate;
using PlanIt.Infrastructure.Authentication;

namespace PlanIt.Infrastructure.Common.Mapping;

public static class IdentityMapping
{
    /*
    public static User ToUser(this ApplicationUser applicationUser) => (
            User.Create(
            applicationUser.Id,
            applicationUser.Email!
        )
    );
    */

    public static ApplicationUser ToApplicationUser(this User user) => (
        new ApplicationUser {
            Id = user.Id
        }
    );

    public static ApplicationUser ToApplicationUser(this User user, string email) => (
        new ApplicationUser {
            Id = user.Id,
            Email = email,
            UserName = email
        }
    );
}