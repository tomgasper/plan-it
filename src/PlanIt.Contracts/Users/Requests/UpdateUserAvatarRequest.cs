
using Microsoft.AspNetCore.Http;

namespace PlanIt.Contracts.Users.Requests;

public record UpdateUserAvatarRequest(
    IFormFile Avatar
);