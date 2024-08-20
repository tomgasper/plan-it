using Microsoft.AspNetCore.Mvc;
using PlanIt.Contracts.Authenthication;
using PlanIt.Application.Services;
using PlanIt.Application.Services.Authentication;
using Microsoft.AspNetCore.Components.Forms;

namespace PlanIt.WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthenthicationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenthicationController(IAuthenticationService authenticationService)
    {
            _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email,request.Password);

        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);

        return Ok(response);
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email,request.Password);

        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);

        return Ok(response);
    }
}