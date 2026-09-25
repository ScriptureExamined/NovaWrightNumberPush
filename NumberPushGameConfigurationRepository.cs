using System.Text.Json;

namespace NovaWrightNumberPush
{
    public class NumberPushGameConfigurationRepository
    {
        private readonly string configurationFilePath;

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public NumberPushGameConfigurationRepository()
        {
            string gameFolder = Path.Combine(AppContext.BaseDirectory, "Game");

            Directory.CreateDirectory(gameFolder);

            configurationFilePath = Path.Combine(gameFolder, "GameConfiguration.json");
        }

        public NumberPushGameConfiguration Load()
        {
            if (!File.Exists(configurationFilePath))
            {
                NumberPushGameConfiguration defaultConfiguration =
                    new NumberPushGameConfiguration();

                Save(defaultConfiguration);

                return defaultConfiguration;
            }

            string json = File.ReadAllText(configurationFilePath);

            NumberPushGameConfiguration? loadedConfiguration =
                JsonSerializer.Deserialize<NumberPushGameConfiguration>(json, Options);

            return loadedConfiguration ?? new NumberPushGameConfiguration();
        }

        public void Save(NumberPushGameConfiguration configuration)
        {
            string json = JsonSerializer.Serialize(configuration, Options);

            File.WriteAllText(configurationFilePath, json);
        }
    }
}
