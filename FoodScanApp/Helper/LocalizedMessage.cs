using System.Resources;

namespace FoodScanApp.Helper
{
    public class LocalizedMessage
    {
        public string GetLocalizedMessage(string key, int language)
        {
            var cultureInfo = language switch
            {
                1 => new System.Globalization.CultureInfo("sv-SE"), // Swedish
                2 => new System.Globalization.CultureInfo("en-US"), // English
                _ => new System.Globalization.CultureInfo("sv-SE")  // Default to English
            };

            // Use a single ResourceManager pointing to the base "Resource"
            var resourceManager = new ResourceManager("FoodScanApp.Resources.Resource", typeof(LocalizedMessage).Assembly);

            // Return the localized string
            return resourceManager.GetString(key, cultureInfo);
        }
    }
}