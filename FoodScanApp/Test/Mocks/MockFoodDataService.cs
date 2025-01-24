using FoodScanApp.DTOs;
using FoodScanApp.Helper;
using FoodScanApp.Services;


namespace FoodScanApp.Test.Mocks
{
    public class MockFoodDataService : IFoodDataService
    {
        private FoodItemDTO _foodResponseDTO;
        private readonly LocalizedMessage _localizedMessage;

        public MockFoodDataService(LocalizedMessage localizedMessage)
        {
            _localizedMessage = localizedMessage;
        }

        public Task<List<FoodItemDTO>> GetAllFoodItemsAsync()
        {
            return Task.FromResult(new List<FoodItemDTO>
            {
                new FoodItemDTO
                {
                    FoodItemId = 0,
                    LivsmedelsTypId = 2,
                    Nummer = 2073,
                    Namn = "Peanut sauce",
                    VetenskapligtNamn = "",
                    Ingredienser = new List<IngredientDTO>()
                },
                new FoodItemDTO
                {
                    FoodItemId = 1,
                    LivsmedelsTypId = 2,
                    Nummer = 2074,
                    Namn = "Peanut butter",
                    VetenskapligtNamn = "",
                    Ingredienser = new List<IngredientDTO>()
                }

            });
        }

        public Task<FoodItemDTO> GetFoodItemByFoodIdAsync(int foodId, int language)
        {
            var localizedMessage = new LocalizedMessage();

            var mockFoodItem = new FoodItemDTO
            {
                FoodItemId = 0,
                LivsmedelsTypId = 2,
                Nummer = 2073,
                Namn = "Peanut sauce",
                VetenskapligtNamn = "",
                Ingredienser = new List<IngredientDTO>()
            };

            
            if (foodId != mockFoodItem.Nummer)
            {
                var errorMessage = localizedMessage.GetLocalizedMessage("FoodItemNotFound", language);

                // Simulate not found exception
                throw new KeyNotFoundException($"{errorMessage}{foodId}");
            }

            return Task.FromResult(mockFoodItem);
        }      


        public Task<List<IngredientDTO>> GetIngredientsByFoodIdAsync(int foodId, int language)
        {
            return Task.FromResult(new List<IngredientDTO>
            {
                new IngredientDTO
                {
                    IngredientId = 0,
                    Nummer = 1893,
                    Namn = "Brown sugar",
                    VattenFaktor = 1,
                    FettFaktor = 1,
                    ViktForeTillagning = 26,
                    ViktEfterTillagning = 26,
                    Tillagningsfaktor = "Dry goods",
                    RetentionsFaktorer = new List<RetentionFactor>
                    {
                        new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.6M },
                    },
                    EuroFIRkod = null
                },
                new IngredientDTO
                {
                    IngredientId = 0,
                    Nummer = 1590,
                    Namn = "Coconut milk",
                    VattenFaktor = 0.79M,
                    FettFaktor = 1,
                    ViktForeTillagning = 200,
                    ViktEfterTillagning = 171.44M,
                    Tillagningsfaktor = "Fluids, boiled",
                    RetentionsFaktorer = new List<RetentionFactor>
                    {
                        new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Thiamin", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Riboflavin", Faktor = 0.95M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.95M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Folate, total", Faktor = 0.8M }
                    },
                    EuroFIRkod = null
                }
            });
        }

        public Task<FoodItemDTO> GetFoodItemWithIngredientsAsync(int foodId, int language)
        {
            return Task.FromResult(new FoodItemDTO
            {
                FoodItemId = 0,
                LivsmedelsTypId = 2,
                Nummer = 2073,
                Namn = "Peanut sauce",
                VetenskapligtNamn = "",
                Ingredienser = new List<IngredientDTO>
        {
            new IngredientDTO
            {
                IngredientId = 0,
                Nummer = 1893,
                Namn = "Brown sugar",
                VattenFaktor = 1,
                FettFaktor = 1,
                ViktForeTillagning = 26,
                ViktEfterTillagning = 26,
                Tillagningsfaktor = "Dry goods",
                RetentionsFaktorer = new List<RetentionFactor>
                {
                    new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.9M },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.6M },
                },
                EuroFIRkod = null
            },
            new IngredientDTO
            {
                IngredientId = 0,
                Nummer = 1590,
                Namn = "Coconut milk",
                VattenFaktor = 0.79M,
                FettFaktor = 1,
                ViktForeTillagning = 200,
                ViktEfterTillagning = 171.44M,
                Tillagningsfaktor = "Fluids, boiled",
                RetentionsFaktorer = new List<RetentionFactor>
                {
                    new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                    new RetentionFactor { NaringsamnesNamn = "Thiamin", Faktor = 0.9M },
                    new RetentionFactor { NaringsamnesNamn = "Riboflavin", Faktor = 0.95M },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.95M },
                    new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.9M },
                    new RetentionFactor { NaringsamnesNamn = "Folate, total", Faktor = 0.8M }
                },
                EuroFIRkod = null
            }
        }
            });
        }
    }
}