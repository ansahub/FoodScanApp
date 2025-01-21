/*using FoodScanApp.Controllers;
using FoodScanApp.DTOs;
using FoodScanApp.Models;
using FoodScanApp.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FoodScanApp.Test.Mocks
{
    public class MockFoodDataService : IFoodDataService
    {
        private FoodItemDTO _foodResponseDTO;

        public Task<List<FoodItemDTO>> GetAllFoodItemsAsync()
        {
            return Task.FromResult(new List<FoodItemDTO>
            {
                new FoodItemDTO
                {
                    LivsmedelsTypId = 1,                    
                    Nummer = 1,                    
                    Namn = "Test Livsmedel",
                    VetenskapligtNamn = "Testus Livsmedelus",
                    
                    Ingredienser = new List<IngredientDTO>
                {
                    new IngredientDTO
                    {
                        Nummer = 1,
                        Namn = "Salt",
                        VattenFaktor = 0.1,
                        FettFaktor = 0.0,
                        ViktForeTillagning = 100,
                        ViktEfterTillagning = 90,
                        Tillagningsfaktor = "10%",
                        RetentionsFaktorer = new List<RetentionFactor>
                        {
                            new RetentionFactor { NaringsamnesNamn = "Salt", Faktor = "0.8" },
                            new RetentionFactor { NaringsamnesNamn = "Vatten", Faktor = "0.9" }
                        },
                        EuroFIRkod = "E123"
                    },
                    new IngredientDTO
                    {
                        Nummer = 2,
                        Namn = "Sugar",
                        VattenFaktor = 0.0,
                        FettFaktor = 0.0,
                        ViktForeTillagning = 100,
                        ViktEfterTillagning = 100,
                        Tillagningsfaktor = "0%",
                        RetentionsFaktorer = new List<RetentionFactor>(),
                        EuroFIRkod = "E124"
                    }
                }
            },
            new FoodItemDTO
            {
                LivsmedelsTypId = 2,               
                Nummer = 2,                
                Namn = "Test Livsmedel 2",
                VetenskapligtNamn = "Testus Livsmedelus II",
            
                Ingredienser = new List<IngredientDTO>
                {
                    new IngredientDTO
                    {
                        Nummer = 3,
                        Namn = "Flour",
                        VattenFaktor = 0.12,
                        FettFaktor = 0.02,
                        ViktForeTillagning = 500,
                        ViktEfterTillagning = 500,
                        Tillagningsfaktor = "0%",
                        RetentionsFaktorer = new List<RetentionFactor>
                        {
                            new RetentionFactor { NaringsamnesNamn = "Protein", Faktor = "0.85" }
                        },
                        EuroFIRkod = "E125"
                    }
                }
            }

            });
        }

        public Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId)
        {
            throw new NotImplementedException();
        }

        public Task<FoodItemDTO> GetFoodItemWithIngredientsAsync(int foodId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId)
        {
            throw new NotImplementedException();
        }
    }
}
*/