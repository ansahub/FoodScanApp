/*using FoodScanApp.Controllers;
using FoodScanApp.DTOs;
using FoodScanApp.Models;
using FoodScanApp.Services;
using FoodScanApp.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FoodScanApp.Test.UnitTests
{
    public class FoodItemControllerTest
    {
        [Fact]
        public async Task GetAllFoodItems_ReturnsOkResult()
        {
            // Arrange
            var mockService = new MockFoodDataService();
            var controller = new FoodItemController(mockService);

            // Act
            var result = await controller.GetAllFoodItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<FoodItem>>(okResult.Value);
            Assert.NotEmpty(value);
        }


        [Fact]
        public async Task GetAllFoodItems_ReturnsNotFound_WhenNoFoodItems()
        {
            // Arrange
            var mockService = new Mock<IFoodDataService>();

            // Mock an empty list as response
            mockService.Setup(service => service.GetAllFoodItemsAsync())
                       .ReturnsAsync(new List<FoodItemDTO>());

            var controller = new FoodItemController(mockService.Object);

            // Act
            var result = await controller.GetAllFoodItems();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Inga livsmedel hittades.", notFoundResult.Value);
        }


        [Fact]
        public async Task GetAllFoodItems_ReturnsCorrectFoodItems()
        {
            // Arrange
            var mockService = new MockFoodDataService();
            var controller = new FoodItemController(mockService);

            // Act
            var result = await controller.GetAllFoodItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<FoodItem>>(okResult.Value);
            var foodItems = value.ToList();

            Assert.NotEmpty(foodItems);
            Assert.Equal(2, foodItems.Count); // Förutsatt att vi mockat 2 FoodItem
            Assert.Equal("Test Livsmedel", foodItems[0].Namn);
            Assert.Equal(1, foodItems[0].Nummer);
            Assert.Equal("Test Livsmedel 2", foodItems[1].Namn);
            Assert.Equal(2, foodItems[1].Nummer);
        }
    }
}
*/