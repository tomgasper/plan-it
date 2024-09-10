using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Users.Queries;
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