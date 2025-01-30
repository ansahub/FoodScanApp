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
                    //FoodItemId = 0,
                    LivsmedelsTypId = 2,
                    Nummer = 2073,
                    Namn = "Peanut sauce",
                    VetenskapligtNamn = "",
                    Ingredienser = new List<IngredientDTO>()
                },
                new FoodItemDTO
                {
                    //FoodItemId = 1,
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
                //FoodItemId = 0,
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
                    Namn = "Brown sugar",
                    VattenFaktor = 1,
                    FettFaktor = 1,
                    ViktForeTillagning = 26,
                    ViktEfterTillagning = 26,
                    Tillagningsfaktor = "Dry goods",
                    /*RetentionsFaktorer = new List<RetentionFactor>
                    {
                        new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.6M },
                    },
                    EuroFIRkod = null*/
                },
                new IngredientDTO
                {
                    IngredientId = 0,
                    Namn = "Coconut milk",
                    VattenFaktor = 0.79M,
                    FettFaktor = 1,
                    ViktForeTillagning = 200,
                    ViktEfterTillagning = 171.44M,
                    Tillagningsfaktor = "Fluids, boiled",
                    /*RetentionsFaktorer = new List<RetentionFactor>
                    {
                        new RetentionFactor { NaringsamnesNamn = "Potassium, K", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Thiamin", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Riboflavin", Faktor = 0.95M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin C", Faktor = 1 },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-12", Faktor = 0.95M },
                        new RetentionFactor { NaringsamnesNamn = "Vitamin B-6", Faktor = 0.9M },
                        new RetentionFactor { NaringsamnesNamn = "Folate, total", Faktor = 0.8M }
                    },
                    EuroFIRkod = null*/
                }
            });
        }

        public Task<List<RavarorDTO>> GetRavarorByFoodIdAsync(int foodId, int language)
        {
            return Task.FromResult(new List<RavarorDTO>
            {
                new RavarorDTO
                {
                    Namn = "Peanut",
                    Tillagning = "Cooked",
                    Andel = 22,
                    Faktor = 1,
                    OmraknadTillRa = 22                   
                },
                new RavarorDTO
                {
                    Namn = "Refined beet sugar",
                    Tillagning = "Uncooked",
                    Andel = 78,
                    Faktor = 1,
                    OmraknadTillRa = 4
                }
            });
        }

        public Task<FoodItemDTO> GetFoodItemWithIngredientsAndRavarorByFoodIdAsync(int foodId, int language)
        {
            var ravaror = new List<RavarorDTO>
            {
                new RavarorDTO { Namn = "Refined beet sugar", Andel = 15, Faktor = 1.0M, OmraknadTillRa = 15 },
                new RavarorDTO { Namn = "Coconut", Andel = 30, Faktor = 1.0M, OmraknadTillRa = 30 },
                new RavarorDTO { Namn = "Peanuts", Andel = 20, Faktor = 1.0M, OmraknadTillRa = 20 },
                new RavarorDTO { Namn = "Water", Andel = 35, Faktor = 1.0M, OmraknadTillRa = 35 }
            };

            var analysis = AnalyzeFoodItem(ravaror, language);

            return Task.FromResult(new FoodItemDTO
            {
                LivsmedelsTypId = 2,
                Nummer = 2073,
                Namn = "Peanut sauce",
                VetenskapligtNamn = "",
                Ingredienser = new List<IngredientDTO>
        {
            new IngredientDTO
            {
                IngredientId = 0,
                Namn = "Brown sugar",
                VattenFaktor = 1,
                FettFaktor = 1,
                ViktForeTillagning = 26,
                ViktEfterTillagning = 26,
                Tillagningsfaktor = "Dry goods"
            },
            new IngredientDTO
            {
                IngredientId = 0,
                Namn = "Coconut milk",
                VattenFaktor = 0.79M,
                FettFaktor = 1,
                ViktForeTillagning = 200,
                ViktEfterTillagning = 171.44M,
                Tillagningsfaktor = "Fluids, boiled"
            }
        },
                Ravaror = ravaror,
                Analysis = analysis
            });
        }

        private FoodItemAnalysisDTO AnalyzeFoodItem(List<RavarorDTO> ravaror, int language)
        {
            // Calculate total percentage of ingredients
            double totalPercentage = ravaror.Sum(i => i.Andel);

            string highSugarMessage = _localizedMessage.GetLocalizedMessage("HighSugarContent", language);
            string lowSugarMessage = _localizedMessage.GetLocalizedMessage("LowSugarContent", language);

            // Check for high sugar content
            var sugar = ravaror.FirstOrDefault(i =>
                i.Namn.IndexOf("sugar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                i.Namn.IndexOf("socker", StringComparison.OrdinalIgnoreCase) >= 0);

            var sugarPercentage = sugar.Andel;

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

    }
}