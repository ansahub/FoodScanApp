using FoodScanApp.DTOs;
using FoodScanApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodScanApp.Services
{
    public interface IFoodDataService
    {
        Task<List<FoodItemDTO>> GetAllFoodItemsAsync();
        Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId);
        Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId);
        Task<FoodItemDTO> GetFoodItemWithIngredientsAsync(int foodId);
    }
}
