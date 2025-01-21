using FoodScanApp.Models;

namespace FoodScanApp.DTOs
{
    /// <summary>
    /// Vad vill jag VISA?
    /// Lista på livsmedel (ex Bregott smör, Ingedienser + Någon Fakta om ingredienserna)
    /// </summary>
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }
        public int LivsmedelsTypId { get; set; }
        public int Nummer { get; set; }
        public string Namn { get; set; }
        public string VetenskapligtNamn { get; set; }
        public List<IngredientDTO> Ingredienser { get; set; }
    }
}
