using FoodScanApp.DTOs;

namespace FoodScanApp.Models
{
    public class Ingredient
    {
        public int Nummer { get; set; }
        public string Namn { get; set; }
        public double VattenFaktor { get; set; }
        public double FettFaktor { get; set; }
        public int ViktForeTillagning { get; set; }
        public int ViktEfterTillagning { get; set; }
        public string Tillagningsfaktor { get; set; }
        public List<RetentionFactor> RetentionsFaktorer { get; set; }
        public string EuroFIRkod { get; set; }

    }
}
