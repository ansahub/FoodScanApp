using System.Net;
using System.Runtime;
using FoodScanApp.DTOs;
using FoodScanApp.Helper;
using FoodScanApp.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoodScanApp.Services
{
    /// <summary>
    /// Hanterar all business logic
    /// och kommunikation med externa resurser 
    /// (API:er, databaser, etc.).
    /// </summary>
    public class FoodDataService : IFoodDataService
    {
        private readonly HttpClient _httpClient;
        private readonly FoodApiSettings _settings;
        private const string LivsMedelKey = "livsmedel";

        public FoodDataService(HttpClient httpClient, IOptions<FoodApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        /// <summary>
        /// May have to remove this later, not necessary to have
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<FoodItemDTO>> GetAllFoodItemsAsync()
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}?offset=0&limit=5&sprak=1"; //Make "sprak" dynamic as variable/input 1=Swe, 2=eng
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Fel vid hämtning av livsmedel: " + response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new Exception("Responsen är null eller tom" + response.StatusCode);
            }

            // Deserialize the full response into a JObject
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonResponse);

            if (jsonObject[LivsMedelKey] == null || !jsonObject[LivsMedelKey].HasValues)
            {
                throw new Exception($"'{LivsMedelKey}' saknas eller innehåller ingen data");
            }
            var foodItemsArray = jsonObject[LivsMedelKey]?.ToObject<List<FoodItemDTO>>();
         
            return foodItemsArray;
        }

        /// <summary>
        /// Get Livsmedel by livsmedelsId/FoodId
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/?sprak=1"; //Make "sprak" dynamic as variable/input 1=Swe, 2=eng
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException($"Livsmedel med Livsmedelsnummer {foodId} hittades inte.");
                }

                throw new HttpRequestException($"Fel vid försök att hämta livsmedel med livsmedelnummer: {foodId} {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new Exception("The response is null or empty.");
            }

            return JsonConvert.DeserializeObject<FoodItemDTO>(jsonResponse);
        }


        /// <summary>
        /// Get ingredienser by LivsmedelsId/FoodId
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/ingredienser?sprak=1";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException($"Ingredienser för livsmedel med livsmedelnummer {foodId} hittades inte.");
                }

                throw new HttpRequestException($"Fel vid hämtning av ingredienser: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new List<IngredientDTO>(); // Return an empty list if null or empty
            }

            var ingredient = JsonConvert.DeserializeObject<List<IngredientDTO>>(jsonResponse);
            return ingredient ?? new List<IngredientDTO>();
        }

        /// <summary>
        /// Get Livsmedel with its ingrediens by LivsmdelsId/FoodId
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        public async Task<FoodItemDTO> GetFoodItemWithIngredientsAsync(int foodId)
        {
            // Fetch FoodItem
            var foodItem = await GetFoodItemByFoodIdAsync(foodId);

            // Fetch Ingredients based on the FoodItem
            var ingredients = await GetIngredientsByFoodIdAsync(foodId);

            // Add ingredients to the FoodItem
            foodItem.Ingredienser = ingredients;

            return foodItem;
        }

   
    }
}
