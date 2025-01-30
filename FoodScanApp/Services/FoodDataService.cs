using System.Net;
using System.Runtime;
using FoodScanApp.DTOs;
using FoodScanApp.Helper;
using FoodScanApp.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

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
        private readonly LocalizedMessage _localizedMessage;
        private const string LivsMedelKey = "livsmedel";

        public FoodDataService(HttpClient httpClient, IOptions<FoodApiSettings> settings, LocalizedMessage localizedMessage)
        {
            _httpClient = httpClient;
            _settings = settings.Value; //URL and endpoint settings
            _localizedMessage = localizedMessage;
        }

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


        public async Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId, int language)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/?sprak={language}"; // 1=Swe, 2=eng
            var response = await _httpClient.GetAsync(url);

            GetErrorAndExceptionMessageIfIdNotFound(response, foodId, language);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new Exception("The response is null or empty.");
            }

            return JsonConvert.DeserializeObject<FoodItemDTO>(jsonResponse);
        }


        public async Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId, int language)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/ingredienser?sprak={language}";
            var response = await _httpClient.GetAsync(url);

            GetErrorAndExceptionMessageIfIdNotFound(response, foodId, language);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new List<IngredientDTO>(); // Return an empty list if null or empty
            }

            var ingredient = JsonConvert.DeserializeObject<List<IngredientDTO>>(jsonResponse);
            return ingredient ?? new List<IngredientDTO>();
        }


        public async Task<List<RavarorDTO>> GetRavarorByFoodIdAsync(int foodId, int language)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/ravaror?sprak={language}";
            var response = await _httpClient.GetAsync(url);

            GetErrorAndExceptionMessageIfIdNotFound(response, foodId, language);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new List<RavarorDTO>();
            }

            var ingredient = JsonConvert.DeserializeObject<List<RavarorDTO>>(jsonResponse);
            return ingredient ?? new List<RavarorDTO>();
        }



        public async Task<FoodItemDTO> GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(int foodId, int language)
        {
            // Fetch FoodItem
            var foodItem = await GetFoodItemByFoodIdAsync(foodId, language);

            // Fetch Ingredients based on the FoodItem
            var ingredients = await GetIngredientsByFoodIdAsync(foodId, language);

            // Fetch Råvaror based on the FoodItem
            var ravaror = await GetRavarorByFoodIdAsync(foodId, language);

            // Add ingredients and ravaror to the FoodItem
            foodItem.Ingredienser = ingredients;
            foodItem.Ravaror = ravaror;

            // Perform the analysis
            foodItem.Analysis = AnalyzeFoodItem(ravaror, language);

            return foodItem;
        }



        private FoodItemAnalysisDTO AnalyzeFoodItem(List<RavarorDTO> ravaror, int language)
        {
            // Calculate total percentage of ingredients
            double totalPercentage = ravaror.Sum(i => i.Andel);

            string highSugarMessage = _localizedMessage.GetLocalizedMessage("HighSugarContent", language);
            string lowSugarMessage = _localizedMessage.GetLocalizedMessage("LowSugarContent", language);

            // Check for high sugar content
            var sugar = ravaror.Where(i =>
                i.Namn.IndexOf("sugar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                i.Namn.IndexOf("socker", StringComparison.OrdinalIgnoreCase) >= 0).ToList();          


            var sugarPercentage = sugar.Sum(i => i.Andel);

            if (sugarPercentage > 10)
            {
                return new FoodItemAnalysisDTO
                {
                    SockerProcentHalt = sugarPercentage,
                    SockerWarningText = $"{highSugarMessage} {sugarPercentage}%"
                };
            }

            return new FoodItemAnalysisDTO
            {
                SockerProcentHalt = sugarPercentage,
                SockerWarningText = $"{lowSugarMessage} {sugarPercentage}%"
            };
        }


        public void GetErrorAndExceptionMessageIfIdNotFound(HttpResponseMessage response, int foodId, int language)
        {
            var errorMessage = String.Empty;

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorMessage = _localizedMessage.GetLocalizedMessage("FoodItemNotFound", language);
                    throw new KeyNotFoundException($"{errorMessage}{foodId}");
                }

                errorMessage = _localizedMessage.GetLocalizedMessage("RequestEx", language);
                throw new HttpRequestException($"{errorMessage}{foodId}, {response.StatusCode}");
            }
        }
    }
}
