using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Application.Contracts.Requests;
using UsuariosApi.Application.Services;
using UsuariosApi.Domain.Entities;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<Usuario>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<Usuario>>> GetAll()
        => Ok(await _service.ListarAsync());

    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Usuario>> GetByEmail([FromRoute] string email)
    {
        var usuario = await _service.ObterPorEmailAsync(email);
        return usuario is null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Usuario>> Post([FromBody] CreateUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.CriarAsync(request);
        if (!result.Success)
            return Conflict(new { message = result.Error });

        return CreatedAtAction(
            nameof(GetByEmail),
            new { email = result.Data!.Email },
            result.Data);
    }

    [HttpPut("email/{email}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put([FromRoute] string email, [FromBody] UpdateUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var existe = await _service.ObterPorEmailAsync(email);
        if (existe is null)
            return NotFound(new { message = "Usuário não encontrado." });

        var result = await _service.AtualizarAsync(email, request);
        return result.Success
            ? NoContent()
            : Conflict(new { message = result.Error });
    }

    [HttpPatch("email/{email}")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Usuario>> Patch([FromRoute] string email, [FromBody] PatchUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.AtualizarParcialAsync(email, request);
        if (!result.Success && result.Error == "Usuário não encontrado.")
            return NotFound(new { message = result.Error });

        if (!result.Success)
            return Conflict(new { message = result.Error });

        return Ok(result.Data);
    }

    [HttpDelete("email/{email}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] string email)
        => await _service.RemoverAsync(email)
            ? NoContent()
            : NotFound(new { message = "Usuário não encontrado." });
}
