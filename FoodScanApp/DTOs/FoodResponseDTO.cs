using FoodScanApp.Models;

namespace FoodScanApp.DTOs
{
    /// <summary>
    /// Vad vill jag VISA?
    /// Lista på livsmedel (ex Bregott smör, Ingedienser + Någon Fakta om ingredienserna)
    /// </summary>
    public class FoodResponseDTO
    {
        public List<FoodItem> Livsmedel { get; set; }
 
    }
}
