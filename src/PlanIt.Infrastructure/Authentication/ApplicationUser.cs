using Microsoft.AspNetCore.Identity;
using PlanIt.Domain.UserAggregate;

namespace PlanIt.Infrastructure.Authentication;

public class ApplicationUser : IdentityUser<Guid>
{
    
}