using System.Text.Json;
using System.Text.Json.Serialization;

namespace NovaWrightNumberPush
{
    public static class NumberPushLevelSerializer
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        static NumberPushLevelSerializer()
        {
            Options.Converters.Add(new JsonStringEnumConverter());
        }

        public static string Serialize(NumberPushLevel level)
        {
            return JsonSerializer.Serialize(level, Options);
        }

        public static NumberPushLevel Deserialize(string json)
        {
            NumberPushLevel? level = JsonSerializer.Deserialize<NumberPushLevel>(json, Options);

            if (level == null)
            {
                throw new InvalidOperationException("The level data could not be loaded.");
            }

            return level;
        }

        public static NumberPushLevel LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);

            return Deserialize(json);
        }

        public static void SaveToFile(NumberPushLevel level, string filePath)
        {
            string json = Serialize(level);

            File.WriteAllText(filePath, json);
        }
    }
}
