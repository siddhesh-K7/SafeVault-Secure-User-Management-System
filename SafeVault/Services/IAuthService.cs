using SafeVault.DTOs;
namespace SafeVault.Services;

public interface IAuthService
{
    Task<UserResponse?> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}