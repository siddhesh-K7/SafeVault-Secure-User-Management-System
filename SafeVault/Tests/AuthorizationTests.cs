using Xunit;
using SafeVault.Controllers;
using SafeVault.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SafeVault.Tests;

public class AuthorizationTests
{
    [Fact]
    public async Task GetById_UserAccessingOtherUser_ReturnsForbid()
    {
        var mockService = new Mock<IUserService>();
        var controller = new UsersController(mockService.Object);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "User")
        }, "mock"));

        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };

        var result = await controller.GetById(2); // Trying to access user ID 2

        Assert.IsType<ForbidResult>(result);
    }
}