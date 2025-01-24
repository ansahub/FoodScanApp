using FoodScanApp.Models;

namespace FoodScanApp.DTOs
{
    /// <summary>
    /// What do I want to display to the USER
    /// List on Livsmedel (e.g Bregott + Ingedients)
    /// </summary>
    public class FoodItemDTO
    {
        //public int FoodItemId { get; set; }
        public int Nummer { get; set; }
        public int LivsmedelsTypId { get; set; }
        public string Namn { get; set; }
        public string VetenskapligtNamn { get; set; }
        public List<IngredientDTO> Ingredienser { get; set; }
    }

    //TODO: Make a translation of the properties in the class in the FRONTEND!!
    //TODO: Figure out whether to use LivsmedelsTypId or LivsmedelsTyp and what it will be used for!
    //TODO: How can we GET and display Information and health risk? Maybe start with the logic of High Sugar content?
}
