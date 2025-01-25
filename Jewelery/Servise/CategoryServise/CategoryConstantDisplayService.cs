using Jewelery.ViewModels.DTO.Category;
using System.Text.Json;
using System.IO;


namespace Jewelery.Servise.CategoryServise
{
    public class CategoryConstantDisplayService
    {
        private const string FilePath = "ConstantCategotryDisplaySettings.json";
        private readonly IConfiguration _configuration;

        public CategoryConstantDisplayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ConfigurationData GetDisplaySettings() 
        {
            return _configuration.Get<ConfigurationData>();
        }

        public void UpdateDisplaySettings(CategoryDisplay newSettings) 
        {
            var config = GetDisplaySettings();

            config.CategoryDisplay = newSettings;

            SaveConfiguration(config);

        }

        public void SaveConfiguration(ConfigurationData config) 
        {
            var NewConfig = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, NewConfig);        
        }
    }
}
