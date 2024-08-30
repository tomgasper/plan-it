using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Http;

public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    private string? UserId { get; }

    public string TryGetUserId()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            throw new Exception( "Couldn't retrieve User's id from Claims Principal.");
        }

        return UserId;
    }
}