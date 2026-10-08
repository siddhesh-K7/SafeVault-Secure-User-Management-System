using Microsoft.AspNetCore.Mvc;
using SafeVault.DTOs;
using SafeVault.Services;
namespace SafeVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(request);
        if (result == null) return Conflict(new { error = "Username or Email already exists." });

        return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var token = await _authService.LoginAsync(request);
        if (token == null) return Unauthorized(new { error = "Invalid credentials." }); // Generic failure message

        return Ok(new { Token = token });
    }
}