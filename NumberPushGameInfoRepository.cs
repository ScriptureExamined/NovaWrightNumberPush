using System.Text.Json;

namespace NovaWrightNumberPush
{
    public class NumberPushGameInfoRepository
    {
        private readonly string gameInfoFilePath;

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public NumberPushGameInfoRepository()
        {
            string gameFolder = Path.Combine(AppContext.BaseDirectory, "Game");

            Directory.CreateDirectory(gameFolder);

            gameInfoFilePath = Path.Combine(gameFolder, "GameInfo.json");
        }

        public NumberPushGameInfo Load()
        {
            if (!File.Exists(gameInfoFilePath))
            {
                NumberPushGameInfo defaultInfo = new NumberPushGameInfo();

                Save(defaultInfo);

                return defaultInfo;
            }

            string json = File.ReadAllText(gameInfoFilePath);

            NumberPushGameInfo? loadedInfo = JsonSerializer.Deserialize<NumberPushGameInfo>(
                json,
                Options
            );

            return loadedInfo ?? new NumberPushGameInfo();
        }

        public void Save(NumberPushGameInfo gameInfo)
        {
            string json = JsonSerializer.Serialize(gameInfo, Options);

            File.WriteAllText(gameInfoFilePath, json);
        }
    }
}
