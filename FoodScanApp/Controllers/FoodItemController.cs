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

                return Ok(foodResponse); // return a list of FoodItem
            }
            catch (HttpRequestException ex)
            {
                // Return 502 if there is issue with API call
                return StatusCode(StatusCodes.Status502BadGateway, "Error while calling the API" + ex.Message);
            }
            catch (Exception ex)
            {
                // Return 500 for other type of errors
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected internal server error occured. " + ex.Message);
            }

        }

        [HttpGet("{foodId}/{language}")]
        public async Task<IActionResult> GetFoodItemByFoodId(int foodId, int language)
        {
            return await HandleRequestAsync(() => _foodService.GetFoodItemByFoodIdAsync(foodId, language),
            $"Food item with ID {foodId} not found.");
        }


        [HttpGet("{foodId}/ingredients/{language}")]
        public async Task<IActionResult> GetIngredientsByFoodId(int foodId, int language)
        {
            return await HandleRequestAsync(() => _foodService.GetIngredientsByFoodIdAsync(foodId, language),
            $"Food item with ID {foodId} not found.");

        }


        [HttpGet("{foodId}/ravaror/{language}")]
        public async Task<IActionResult> GetRavarorByFoodId(int foodId, int language)
        {
            return await HandleRequestAsync(() => _foodService.GetIngredientsByFoodIdAsync(foodId, language),
            $"Food item with ID {foodId} not found.");

        }


        [HttpGet("{foodId}/ingredients/ravaror/{language}")]
        public async Task<IActionResult> GetFoodItemWithIngredientsAndRavarorByFoodId(int foodId, int language)
        {
            return await HandleRequestAsync(() => _foodService.GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(foodId, language),
            $"Food item with ID {foodId} not found.");
        }


        /// <summary>
        /// Handles requests by executing the function
        /// and catching exceptions in a unified way.
        /// </summary>
        private async Task<IActionResult> HandleRequestAsync<T>(Func<Task<T>> action, string notFoundMessage)
        {
            try
            {
                var result = await action();
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"{notFoundMessage} {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server issue: " + ex.Message);
            }
        }
    }
}