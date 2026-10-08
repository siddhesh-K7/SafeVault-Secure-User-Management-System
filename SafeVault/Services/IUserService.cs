using SafeVault.DTOs;
namespace SafeVault.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task<UserResponse?> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(int id, string displayName);
    Task<bool> DeleteUserAsync(int id);
}