using FoodScanApp.Helper;
using Newtonsoft.Json;

namespace FoodScanApp.DTOs
{
    public class RavarorDTO
    {
        public string Namn { get; set; }
        public string Tillagning { get; set; }
        public int Andel { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal Faktor { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal OmraknadTillRa { get; set; }
    }
}
