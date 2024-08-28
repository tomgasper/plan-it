using Microsoft.AspNetCore.Identity;

namespace PlanIt.Infrastructure.Authentication;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName {get; set;} = null!;
    public string LastName {get;set;} = null!;
}