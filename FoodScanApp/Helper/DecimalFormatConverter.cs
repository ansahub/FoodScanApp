using Newtonsoft.Json;

namespace FoodScanApp.Helper
{
    public class DecimalFormatConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is decimal decimalValue)
            {
                writer.WriteValue(Math.Round(decimalValue, 1));
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert.ToDecimal(reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }
    }

}
