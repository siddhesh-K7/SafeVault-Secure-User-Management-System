using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeVault.DTOs;
using SafeVault.Services;
using System.Security.Claims;
namespace SafeVault.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires authentication for all endpoints
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")] // RBAC: Admin only
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserIdStr == null) return Unauthorized();
        var currentUserId = int.Parse(currentUserIdStr);
        var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

        // RBAC: Users can view their own profile; Admins can view any profile
        if (currentUserRole != "Admin" && currentUserId != id) return Forbid();

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] string newDisplayName)
    {
        var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserIdStr == null) return Unauthorized();
        var currentUserId = int.Parse(currentUserIdStr);
        var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

        if (currentUserRole != "Admin" && currentUserId != id) return Forbid();

        var success = await _userService.UpdateUserAsync(id, newDisplayName);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}