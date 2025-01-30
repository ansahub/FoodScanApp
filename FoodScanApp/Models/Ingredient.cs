using FoodScanApp.DTOs;

namespace FoodScanApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public int Nummer { get; set; }
        public string Namn { get; set; }
        public decimal VattenFaktor { get; set; }
        public decimal FettFaktor { get; set; }
        public decimal ViktForeTillagning { get; set; }
        public decimal ViktEfterTillagning { get; set; }
        public string Tillagningsfaktor { get; set; }
        public List<RetentionFactor> RetentionsFaktorer { get; set; }

    }

    public class RetentionFactor
    {
        public string NaringsamnesNamn { get; set; }
        public decimal Faktor { get; set; }
        public string EuroFIRkod { get; set; }
    }
}
