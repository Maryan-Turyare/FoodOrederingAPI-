using FoodOrederingAPI.Controllers;
using FoodOrederingAPI.Models;
using FoodOrederingAPI.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodOrederingAPI.Tests.Controllers;

public class FoodControllerTests
{
    [Fact]
    public void GetAllFoods_ReturnsOkWithAllFoods()
    {
        var foods = new List<Food>
        {
            new() { Id = 1, Name = "Pizza", Price = 12.99m, Description = "Cheese pizza" },
            new() { Id = 2, Name = "Burger", Price = 8.50m, Description = "Beef burger" }
        };
        var mockFoods = DbSetMockHelper.CreateMockDbSet(foods);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Foods).Returns(mockFoods.Object);

        var controller = new FoodController(mockContext.Object);

        var result = controller.GetFoods();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoods = Assert.IsAssignableFrom<List<Food>>(okResult.Value);
        Assert.Equal(2, returnedFoods.Count);
        Assert.Contains(returnedFoods, f => f.Name == "Pizza");
        Assert.Contains(returnedFoods, f => f.Name == "Burger");
    }

    [Fact]
    public void GetAllFoods_WhenEmpty_ReturnsOkWithEmptyList()
    {
        var foods = new List<Food>();
        var mockFoods = DbSetMockHelper.CreateMockDbSet(foods);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Foods).Returns(mockFoods.Object);

        var controller = new FoodController(mockContext.Object);

        var result = controller.GetFoods();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoods = Assert.IsAssignableFrom<List<Food>>(okResult.Value);
        Assert.Empty(returnedFoods);
    }

    [Fact]
    public void AddFood_ValidFood_ReturnsOkAndPersistsFood()
    {
        var foods = new List<Food>();
        var mockFoods = DbSetMockHelper.CreateMockDbSet(foods);

        var mockContext = DbContextMockHelper.CreateMockContext();
        mockContext.Setup(c => c.Foods).Returns(mockFoods.Object);
        mockContext.Setup(c => c.SaveChanges()).Returns(1);

        var controller = new FoodController(mockContext.Object);
        var newFood = new Food
        {
            Name = "Pasta",
            Price = 10.99m,
            Description = "Spaghetti marinara"
        };

        var result = controller.AddFood(newFood);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFood = Assert.IsType<Food>(okResult.Value);
        Assert.Equal("Pasta", returnedFood.Name);
        Assert.Equal(10.99m, returnedFood.Price);
        Assert.Single(foods);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
}
