namespace FoodScanApp.DTOs
{
    public class IngredientResponseDTO
    {
        public int Nummer { get; set; }
        public string Namn { get; set; }
        public double VattenFaktor { get; set; }
        public double FettFaktor { get; set; }
        public int ViktForeTillagning { get; set; }
        public int ViktEfterTillagning { get; set; }
        public string Tillagningsfaktor { get; set; }
        public List<RetentionFactor> RetentionsFaktorer { get; set; }
    }

    public class RetentionFactor
    {
        public string NaringsamnesNamn { get; set; }
        public string Faktor { get; set; }
    }
}
