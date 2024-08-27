using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanIt.Application.Projects.CreateProject.Commands;
using PlanIt.Contracts.Projects;

namespace PlanIt.WebApi.Controllers;

[AllowAnonymous]
[Route("projectOwners/{projectOwnerId}/project")]
public class ProjectController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public ProjectController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(
        CreateProjectRequest request,
        string projectOwnerId
    )
    {
        var command = _mapper.Map<CreateProjectCommand>((request, projectOwnerId));

        var createProjectResult = await _mediator.Send(command);

        if (createProjectResult.IsFailed)
        {
            return Problem(createProjectResult.Errors);
        }

        return Ok(_mapper.Map<ProjectResponse>(createProjectResult.Value));
    }
}