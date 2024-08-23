using Microsoft.AspNetCore.Mvc;
using PlanIt.Contracts.Authenthication;
using MediatR;
using PlanIt.Application.Authentication.Commands.Register;
using PlanIt.Application.Authentication.Commands.Queries.Login;
using MapsterMapper;

namespace PlanIt.WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthenthicationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenthicationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
{
    var command = _mapper.Map<RegisterCommand>(request);
    var authResult = await _mediator.Send(command);

    if (authResult.IsFailed)
    {
        return Problem(authResult.Errors);
    }

    var response = _mapper.Map<AuthenticationResponse>(authResult.Value);
    return Ok(response);
}

    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        if (authResult.IsFailed)
        {
        return Problem(authResult.Errors);
        }

        var response = _mapper.Map<AuthenticationResponse>(authResult);

        return Ok(response);
    }
}