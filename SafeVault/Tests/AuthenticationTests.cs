using Moq;
using Xunit;
using SafeVault.Controllers;
using SafeVault.Services;
using SafeVault.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SafeVault.Tests;

public class AuthenticationTests
{
    [Fact]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        var mockService = new Mock<IAuthService>();
        mockService.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>())).ReturnsAsync("valid.jwt.token");
        var controller = new AuthController(mockService.Object);

        var result = await controller.Login(new LoginRequest { UsernameOrEmail = "test", Password = "Valid1!@" });

        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var mockService = new Mock<IAuthService>();
        mockService.Setup(s => s.LoginAsync(It.IsAny<LoginRequest>())).ReturnsAsync((string?)null);
        var controller = new AuthController(mockService.Object);

        var result = await controller.Login(new LoginRequest { UsernameOrEmail = "test", Password = "Wrong1!@" });

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}