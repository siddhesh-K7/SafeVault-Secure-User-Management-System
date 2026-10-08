using Microsoft.EntityFrameworkCore;
using SafeVault.Data;
using SafeVault.DTOs;
using System.Web; // for HtmlEncode
namespace SafeVault.Services;

public class UserService : IUserService
{
    private readonly SafeVaultDbContext _context;

    public UserService(SafeVaultDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        return await _context.Users.Select(u => new UserResponse {
            Id = u.Id, Username = u.Username, Email = u.Email, DisplayName = u.DisplayName, Role = u.Role
        }).ToListAsync();
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        return new UserResponse { Id = user.Id, Username = user.Username, Email = user.Email, DisplayName = user.DisplayName, Role = user.Role };
    }

    public async Task<bool> UpdateUserAsync(int id, string displayName)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        
        // Basic XSS Mitigation - encode user inputs before saving
        user.DisplayName = HttpUtility.HtmlEncode(displayName);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}