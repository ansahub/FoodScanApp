using FoodScanApp.DTOs;
using FoodScanApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodScanApp.Services
{
    public interface IFoodDataService
    {
        Task<FoodResponseDTO> GetAllFoodItemsAsync();
        Task<List<IngredientResponseDTO>> GetIngredientsByFoodIdAsync(int foodId);
        Task<FoodItem> GetFoodItemByFoodIdAsync(int foodId);
    }
}
