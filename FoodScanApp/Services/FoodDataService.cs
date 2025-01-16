using System.Net;
using System.Runtime;
using FoodScanApp.DTOs;
using FoodScanApp.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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

        public FoodDataService(HttpClient httpClient, IOptions<FoodApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<FoodResponseDTO> GetAllFoodItemsAsync()
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}?offset=0&limit=5&sprak=1";
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
            return JsonConvert.DeserializeObject<FoodResponseDTO>(jsonResponse);
        }

        public async Task<FoodItem> GetFoodItemByFoodIdAsync(int foodId)
        {
            var url = $"{_settings.BaseUrl}{_settings.Endpoint}/{foodId}/?sprak=1";
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

            return JsonConvert.DeserializeObject<FoodItem>(jsonResponse);
        }

        public async Task<List<IngredientResponseDTO>> GetIngredientsByFoodIdAsync(int foodId)
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
                return new List<IngredientResponseDTO>(); // Returnera en tom lista om svaret är tomt
            }

            var ingredient = JsonConvert.DeserializeObject<List<IngredientResponseDTO>>(jsonResponse);
            return ingredient ?? new List<IngredientResponseDTO>();
        }
    }
}
