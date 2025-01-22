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
                _ => new System.Globalization.CultureInfo("en-US")  // Default to English
            };

            // Create ResourceManager for each language
            ResourceManager resourceManager;

            // Handle different resource files
            if (cultureInfo.Name == "sv-SE")
            {
                // For Swedish, use Resource.sv.resx
                resourceManager = new ResourceManager("FoodScanApp.Resources.Resource", typeof(LocalizedMessage).Assembly);
            }
            else
            {
                // For English or default, use Resource.resx
                resourceManager = new ResourceManager("FoodScanApp.Resources.Resource", typeof(LocalizedMessage).Assembly);
            }

            // Return the localized string
            return resourceManager.GetString(key, cultureInfo);
        }
    }
}
