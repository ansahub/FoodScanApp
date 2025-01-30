using FoodScanApp.Helper;
using Newtonsoft.Json;

namespace FoodScanApp.DTOs
{
    public class IngredientDTO
    {
        public int IngredientId { get; set; }
        public string Namn { get; set; }

        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal VattenFaktor { get; set; }

        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal FettFaktor { get; set; }

        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal ViktForeTillagning { get; set; }

        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal ViktEfterTillagning { get; set; }
        public string Tillagningsfaktor { get; set; }

        //public List<RetentionFactor> RetentionsFaktorer { get; set; }
    }

    //TODO: May not be needed!
    public class RetentionFactor
    {
        public string NaringsamnesNamn { get; set; }

        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal Faktor { get; set; }
        public string EuroFIRkod { get; set; }
    }
}
