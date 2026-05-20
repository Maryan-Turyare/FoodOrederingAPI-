using FoodOrederingAPI.Controllers;
using FoodOrederingAPI.Models;
using FoodOrederingAPI.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FoodOrederingAPI.Tests.Controllers;

public class AuthControllerTests
{
    private static Mock<IConfiguration> CreateJwtConfigurationMock()
    {
        var mockConfig = new Mock<IConfiguration>();

        mockConfig.Setup(c => c["Jwt:Key"])
            .Returns("ThisIsMyVeryLongSecretKeyForJWTAuthentication123456789");
        mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("FoodOrederingAPI");
        mockConfig.Setup(c => c["Jwt:Audience"]).Returns("FoodOrederingUsers");

        return mockConfig;
    }

    [Fact]
    public void Register_ValidUser_ReturnsOkAndPersistsUser()
    {
        var users = new List<User>();
        var mockUsers = DbSetMockHelper.CreateMockDbSet(users);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Users).Returns(mockUsers.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        var controller = new AuthController(mockContext.Object, CreateJwtConfigurationMock().Object);
        var newUser = new User { Username = "testuser", Password = "password123" };

        var result = controller.Register(newUser);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("User registered successfully", okResult.Value);
        Assert.Single(users);
        Assert.Equal("testuser", users[0].Username);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsOkWithToken()
    {
        var users = new List<User>
        {
            new() { Id = 1, Username = "testuser", Password = "password123" }
        };
        var mockUsers = DbSetMockHelper.CreateMockDbSet(users);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Users).Returns(mockUsers.Object);

        var controller = new AuthController(mockContext.Object, CreateJwtConfigurationMock().Object);
        var loginUser = new User { Username = "testuser", Password = "password123" };

        var result = controller.Login(loginUser);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        var tokenProperty = okResult.Value!.GetType().GetProperty("token");
        Assert.NotNull(tokenProperty);
        var token = tokenProperty.GetValue(okResult.Value) as string;
        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var users = new List<User>
        {
            new() { Id = 1, Username = "testuser", Password = "password123" }
        };
        var mockUsers = DbSetMockHelper.CreateMockDbSet(users);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Users).Returns(mockUsers.Object);

        var controller = new AuthController(mockContext.Object, CreateJwtConfigurationMock().Object);
        var loginUser = new User { Username = "testuser", Password = "wrongpassword" };

        var result = controller.Login(loginUser);

        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Invalid credentials", unauthorizedResult.Value);
    }
}
