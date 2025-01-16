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

                if (foodResponse == null || !foodResponse.Livsmedel.Any())
                {
                    // Returnera 404 om inga livsmedel hittas
                    return NotFound("Inga livsmedel hittades.");
                }

                return Ok(foodResponse.Livsmedel); // Returnera bara listan av livsmedel
            }
            catch (HttpRequestException ex)
            {
                // Returnera 502 om det är problem med API-anropet
                return StatusCode(StatusCodes.Status502BadGateway, "Fel vid anrop till API: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Returnera 500 för alla andra typer av fel
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }

        }

        [HttpGet("{foodId}")]
        public async Task<IActionResult> GetFoodItemByFoodId(int foodId)
        {
            try
            {
                var foodItem = await _foodService.GetFoodItemByFoodIdAsync(foodId);
                return Ok(foodItem); // Returnera objektet om det hittas
            }
            catch (KeyNotFoundException ex)
            {
                // Returnera 404 om resursen inte hittades
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                // Returnera 502 om det är problem med API-anropet
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                // Returnera 500 för alla andra typer av fel
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }
        }


        [HttpGet("{foodId}/ingredients")]
        public async Task<IActionResult> GetIngredients(int foodId)
        {
            try
            {
                var ingredients = await _foodService.GetIngredientsByFoodIdAsync(foodId);

                return Ok(ingredients); // Returnera objektet om det hittas
            }
            catch (KeyNotFoundException ex)
            {
                // Returnera 404 om resursen inte hittades
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                // Returnera 502 om det är problem med API-anropet
                return StatusCode(StatusCodes.Status502BadGateway, ex.Message);
            }
            catch (Exception ex)
            {
                // Returnera 500 för alla andra typer av fel
                return StatusCode(StatusCodes.Status500InternalServerError, "Internt serverfel: " + ex.Message);
            }
        }
    }
}