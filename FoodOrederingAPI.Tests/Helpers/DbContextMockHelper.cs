using FoodOrederingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FoodOrederingAPI.Tests.Helpers;

public static class DbContextMockHelper
{
    public static Mock<AppDbContext> CreateMockContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().Options;
        return new Mock<AppDbContext>(options);
    }
}
