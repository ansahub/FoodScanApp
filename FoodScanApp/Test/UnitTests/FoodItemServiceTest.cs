using FoodScanApp.Helper;
using FoodScanApp.Test.Mocks;
using Xunit;

namespace FoodScanApp.Test.UnitTests
{
    public class FoodItemServiceTest
    {
        private readonly MockFoodDataService _mockService;
        private readonly LocalizedMessage _localizedMessage;

        public FoodItemServiceTest()
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
            //Assert.Equal("Potassium, K", result[0].RetentionsFaktorer[0].NaringsamnesNamn);
        }

        [Fact]
        public async Task GetFoodItemWithIngredientsAndRavarorAsync_ValidId_ReturnsFoodItemWithIngredientsAndRavaror()
        {
            // Arrange
            int validFoodId = 2073; // Valid ID from the mock data
            int language = 2; // English

            // Act
            var result = await _mockService.GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(validFoodId, language);

            var ingredientCount = result.Ingredienser.Count();

            // Assert
            Assert.Equal(2, ingredientCount);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Ingredienser);
            Assert.Equal("Coconut milk", result.Ingredienser[1].Namn);
            Assert.Equal("Refined beet sugar", result.Ravaror[0].Namn);
        }

        [Fact]
        public async Task Over10PercentSugarShouldReturnCorrectWarningMessageAndSugarContentPercentage()
        {
            // Arrange
            int validFoodId = 2073; // Valid ID from the mock data
            int language = 2; // English

            // Act
            var result = await _mockService.GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(validFoodId, language);
            var sugarContent = result.Ravaror
                            .Where(r => r.Namn.Contains("sugar", StringComparison.OrdinalIgnoreCase))
                            .ToList();

            var sugarPercentage = sugarContent.Sum(i => i.Andel);

            // Assert
            var expectedMessage = $"High sugar content with total of 15%";
            Assert.Equal(expectedMessage, result.Analysis.SockerWarningText);
            Assert.Equal(15, sugarPercentage);
            
        }
    }
}