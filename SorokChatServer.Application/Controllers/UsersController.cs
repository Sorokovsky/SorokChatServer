using Microsoft.AspNetCore.Mvc;
using SorokChatServer.Application.Contracts.Users;
using SorokChatServer.Application.Interfaces;

namespace SorokChatServer.Application.Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;

    public UsersController(IUsersService service)
    {
        _service = service;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await _service.GetById(id, cancellationToken);
        if (result.IsSuccess) return Ok(result.Value.ToGetUser());
        return BadRequest(result.Error);
    }

    [HttpGet("{email:alpha}")]
    public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var result = await _service.GetByEmail(email, cancellationToken);
        if (result.IsSuccess) return Ok(result.Value.ToGetUser());
        return BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUser newUser, CancellationToken cancellationToken)
    {
        var convertResult = Models.User.Create(newUser);
        if (convertResult.IsFailure) return BadRequest(convertResult.Error);
        var createdResult = await _service.Create(convertResult.Value, cancellationToken);
        if (createdResult.IsFailure) return BadRequest(createdResult.Error);
        return Ok(createdResult.Value.ToGetUser());
    }
}