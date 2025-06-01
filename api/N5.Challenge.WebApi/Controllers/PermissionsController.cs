using MediatR;

using Microsoft.AspNetCore.Mvc;

using N5.Challenge.Application.Commands;
using N5.Challenge.Application.Dtos;
using N5.Challenge.Application.Queries;

namespace N5.Challenge.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return StatusCode(StatusCodes.Status200OK, await _mediator.Send(new GetPermissionsQuery(), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command, CancellationToken cancellationToken)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ModifyPermission(int id, [FromBody] ModifyPermissionCommand command, CancellationToken cancellationToken)
    {

        return StatusCode(StatusCodes.Status200OK, await _mediator.Send(command with { Id = id }, cancellationToken));
    }
}