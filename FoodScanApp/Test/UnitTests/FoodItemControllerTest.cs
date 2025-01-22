using FoodScanApp.Controllers;
using FoodScanApp.DTOs;
using FoodScanApp.Helper;
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
        LocalizedMessage localizedMessage = new LocalizedMessage();
              

        [Fact]
        public async Task ReturnMessageInEnglishWhenNotFindFoodItem()
        {
            // Arrange        
            var mockService = new MockFoodDataService(localizedMessage);

            // Act & Assert
            var foodId = 22; // ID that doesn't exist in the mock
            var language = 2; // English

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => mockService.GetFoodItemByFoodIdAsync(foodId, language)
            );

            // Assert
            var expectedMessage = $"Can't find the food item with the foodId:{foodId}";
            Assert.Equal(expectedMessage, exception.Message);
        }


    }
}