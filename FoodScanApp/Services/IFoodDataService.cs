using FoodScanApp.DTOs;
using FoodScanApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodScanApp.Services
{
    public interface IFoodDataService
    {
        Task<List<FoodItemDTO>> GetAllFoodItemsAsync();
        Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId, int language);
        Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId, int language);
        Task <List<RavarorDTO>> GetRavarorByFoodIdAsync(int foodId, int language);
        Task<FoodItemDTO> GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(int foodId, int language);
    }
}
