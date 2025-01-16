using FoodScanApp.Controllers;
using FoodScanApp.DTOs;
using FoodScanApp.Models;
using FoodScanApp.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FoodScanApp.Test.Mocks
{
    public class MockFoodDataService : IFoodDataService
    {
        private FoodResponseDTO _foodResponseDTO;
        public Task<FoodResponseDTO> GetAllFoodItemsAsync()
        {
            return Task.FromResult(new FoodResponseDTO
            {
                Livsmedel = new List<FoodItem>
        {
            new FoodItem
            {
                LivsmedelsTypId = 1,
                LivsmedelsTyp = "Typ A",
                Nummer = 1,
                Version = "1.0",
                Namn = "Test Livsmedel",
                VetenskapligtNamn = "Testus Livsmedelus",
                Projekt = "Projekt A",
                /*Ingredienser = new List<Ingredient>
                {
                    new Ingredient
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
                    new Ingredient
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
                }*/
            },
            new FoodItem
            {
                LivsmedelsTypId = 2,
                LivsmedelsTyp = "Typ B",
                Nummer = 2,
                Version = "1.1",
                Namn = "Test Livsmedel 2",
                VetenskapligtNamn = "Testus Livsmedelus II",
                Projekt = "Projekt B",
                /*Ingredienser = new List<Ingredient>
                {
                    new Ingredient
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
                }*/
            }
        }
            });
        }

        public Task<FoodItem> GetFoodItemByFoodIdAsync(int foodId)
        {
            throw new NotImplementedException();
        }

        public Task<List<IngredientResponseDTO>> GetIngredientsByFoodIdAsync(int foodId)
        {
            throw new NotImplementedException();
        }
    }
}
