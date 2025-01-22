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
            try
            {
                var foodItem = await _foodService.GetFoodItemByFoodIdAsync(foodId, language);
                return Ok(foodItem);
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server issue: " + ex.Message);
            }
        }


        [HttpGet("{foodId}/ingredients/{language}")]
        public async Task<IActionResult> GetIngredientsByFoodId(int foodId, int language)
        {
            try
            {
                var ingredients = await _foodService.GetIngredientsByFoodIdAsync(foodId, language);

                return Ok(ingredients); // Return a list of Ingredients if found                
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Food item with ID {foodId} not found." + ex.Message);
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

        [HttpGet("{foodId}/livsmedelingrediens/{language}")]
        public async Task<IActionResult> GetFoodItemWithIngredientsByFoodId(int foodId, int language)
        {
            try
            {
                var foodItem = await _foodService.GetFoodItemWithIngredientsAsync(foodId, language);

                return Ok(foodItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Food item with ID {foodId} not found." + ex.Message);
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