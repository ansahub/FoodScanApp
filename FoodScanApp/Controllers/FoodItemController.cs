using System.Globalization;
using FoodScanApp.DTOs;
using FoodScanApp.Models;
using FoodScanApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodScanApp.Controllers
{
    /// <summary>
    /// Ska bara hantera HTTP-anrop 
    /// och returnera lämpliga svar 
    /// (t.ex., 200 OK, 404 Not Found, 500 Internal Server Error).
    /// </summary>

    [ApiController]
    [Route("api/fooditem")]
    public class FoodItemController : Controller
    {
        private readonly IFoodDataService _foodService;

        public FoodItemController(IFoodDataService foodService)
        {
            _foodService = foodService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFoodItems()
        {
            try
            {
                var foodResponse = await _foodService.GetAllFoodItemsAsync();

                if (foodResponse == null)
                {
                    // Return 404 if no FoodItem found
                    return NotFound("Inga livsmedel hittades.");
                }

                return Ok(foodResponse); // return a list of FoodItem
            }
            catch (HttpRequestException ex)
            {
                // Return 502 if there is issue with API call
                return StatusCode(StatusCodes.Status502BadGateway, "Fel vid anrop till API: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Return 500 for other type of errors
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }

        }

        [HttpGet("{foodId}")]
        public async Task<IActionResult> GetFoodItemByFoodId(int foodId)
        {
            try
            {
                var foodItem = await _foodService.GetFoodItemByFoodIdAsync(foodId);
                return Ok(foodItem); // Return the FoodItem object if found
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }
        }


        [HttpGet("{foodId}/ingredients")]
        public async Task<IActionResult> GetIngredientsByFoodId(int foodId)
        {
            try
            {
                var ingredients = await _foodService.GetIngredientsByFoodIdAsync(foodId);

                if (ingredients != null)
                {
                    return Ok(ingredients); // Return a list of Ingredients if found
                }
                throw new KeyNotFoundException($"Food item with ID {foodId} not found.");

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }
        }

        [HttpGet("{foodId}/livsmedelingrediens")]
        public async Task<IActionResult> GetFoodItemWithIngredientsByFoodId(int foodId)
        {
            try
            {
                var foodItem = await _foodService.GetFoodItemWithIngredientsAsync(foodId);

                if (foodItem != null)
                {         
                    return Ok(foodItem); // Return the FoodItem with Ingredients if ok and found
                }
                throw new KeyNotFoundException($"Food item with ID {foodId} not found.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }
        }

    }
}