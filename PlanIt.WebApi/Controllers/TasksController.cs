using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.WebApi.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class TasksController : ApiController{
    [HttpGet]
    public IActionResult LastTask()
    {
        return Ok(Array.Empty<string>());
    }
}