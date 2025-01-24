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
        private readonly MockFoodDataService _mockService;
        private readonly LocalizedMessage _localizedMessage;

        public FoodItemControllerTest()
        {
            _localizedMessage = new LocalizedMessage();
            _mockService = new MockFoodDataService(_localizedMessage);
        }


        [Fact]
        public async Task GetFoodItemByFoodIdAsync_ValidId_ReturnsFoodItem()
        {
            // Arrange
            int validFoodId = 2073; // Valid ID from the mock data
            int language = 2; // English

            // Act
            var result = await _mockService.GetFoodItemByFoodIdAsync(validFoodId, language);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(validFoodId, result.Nummer);
            Assert.Equal("Peanut sauce", result.Namn);
        }

        [Fact]
        public async Task GetFoodItemByFoodIdAsync_InvalidId_ThrowsKeyNotFoundEx_AndEnglishMessage()
        {
            // Arrange        
            var foodId = 22; // ID that doesn't exist in the mock
            var language = 2; // English
            
            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _mockService.GetFoodItemByFoodIdAsync(foodId, language)
            );

            // Assert
            var expectedMessage = $"Can't find the food item with the foodId:{foodId}";
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task GetFoodItemByFoodIdAsync_InvalidId_ThrowsKeyNotFoundEx_AndSweMessage()
        {
            // Arrange        
            var foodId = 22; // ID that doesn't exist in the mock
            var language = 1; // Swedish

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _mockService.GetFoodItemByFoodIdAsync(foodId, language)
            );

            // Assert
            var expectedMessage = $"Kan inte hitta livsmedel med livsmedelsnummer:{foodId}";
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task GetIngredientsByFoodIdAsync_ValidId_ReturnsIngredients()
        {
            // Arrange
            int validFoodId = 2073; // Valid ID from the mock data
            int language = 2; // English

            // Act
            var result = await _mockService.GetIngredientsByFoodIdAsync(validFoodId, language);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Brown sugar", result[0].Namn);
            Assert.Equal("Potassium, K", result[0].RetentionsFaktorer[0].NaringsamnesNamn);
        }

        [Fact]
        public async Task GetFoodItemWithIngredientsAsync_ValidId_ReturnsFoodItemWithIngredients()
        {
            // Arrange
            int validFoodId = 2073; // Valid ID from the mock data
            int language = 2; // English

            // Act
            var result = await _mockService.GetFoodItemWithIngredientsAsync(validFoodId, language);

            var ingredientCount = result.Ingredienser.Count();

            // Assert
            Assert.Equal(2, ingredientCount);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Ingredienser);
            Assert.Equal("Coconut milk", result.Ingredienser[1].Namn);
        }
    }
}