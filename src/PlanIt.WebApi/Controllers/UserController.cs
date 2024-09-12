using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Users.Commands;
using PlanIt.Application.Users.Queries.GetUser;
using PlanIt.Application.Users.Queries.GetUserWorkspace;
using PlanIt.Contracts.Users.Requests;
using PlanIt.WebApi.Common.Mapping;

namespace PlanIt.WebApi.Controllers;

[Route("api/users/{userId}")]
public class UserController : ApiController
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }

    // Add Get User endpoint
    [HttpGet]
    public async Task<IActionResult> GetUser(string userId)
    {
        GetUserQuery query = new(userId);

        var getUserResult = await _mediator.Send(query);

        if (getUserResult.IsFailed)
        {
            return Problem(getUserResult.Errors);
        }

        return Ok(getUserResult.Value.MapToResponse());
    }

    [HttpPatch]
    [Route("avatar")]
    public async Task<IActionResult> UpdateUserAvatar([FromForm] UpdateUserAvatarRequest request, string userId)
    {
        UpdateUserAvatarCommand command = request.MapToCommand(userId);

        var updateUserAvatarResult = await _mediator.Send(command);

        if (updateUserAvatarResult.IsFailed)
        {
            return Problem(updateUserAvatarResult.Errors);
        }

        return Ok(updateUserAvatarResult.Value.MapToResponse());
    }

    [HttpGet]
    [Route("workspaces")]
    public async Task<IActionResult> GetUserWorkspaces(string userId)
    {
        GetUserWorkspacesQuery query = new(userId);

        var getUserWorkspacesResult = await _mediator.Send(query);

        if (getUserWorkspacesResult.IsFailed)
        {
            return Problem(getUserWorkspacesResult.Errors);
        }

        return Ok(getUserWorkspacesResult.Value.MapToResponse());
    }
}