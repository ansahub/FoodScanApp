using FoodScanApp.DTOs;
using FoodScanApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodScanApp.Services
{
    public interface IFoodDataService
    {
        Task<List<FoodItemDTO>> GetAllFoodItemsAsync();
        Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId, int language);
        Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId, int language);
        Task<FoodItemDTO> GetFoodItemWithIngredientsAsync(int foodId, int language);
    }
}
