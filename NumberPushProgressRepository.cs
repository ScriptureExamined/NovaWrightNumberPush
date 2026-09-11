using System.Text.Json;

namespace NovaWrightNumberPush
{
    public class NumberPushProgressRepository
    {
        private readonly string progressFilePath;

        private static readonly JsonSerializerOptions Options =
            new JsonSerializerOptions
            {
                WriteIndented = true
            };

        public NumberPushProgressRepository()
        {
            string dataFolder =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Data");

            Directory.CreateDirectory(dataFolder);

            progressFilePath =
                Path.Combine(
                    dataFolder,
                    "Progress.json");
        }

        public NumberPushProgress Load()
        {
            if (!File.Exists(progressFilePath))
            {
                return new NumberPushProgress();
            }

            string json =
                File.ReadAllText(
                    progressFilePath);

            NumberPushProgress? progress =
                JsonSerializer.Deserialize<NumberPushProgress>(
                    json,
                    Options);

            return progress ??
                new NumberPushProgress();
        }

        public void Save(
            NumberPushProgress progress)
        {
            string json =
                JsonSerializer.Serialize(
                    progress,
                    Options);

            File.WriteAllText(
                progressFilePath,
                json);
        }

        public void RecordCompletion(
            NumberPushProgress progress,
            int levelNumber,
            int pushes)
        {
            NumberPushLevelProgress? levelProgress =
                GetLevelProgress(
                    progress,
                    levelNumber);

            if (levelProgress == null)
            {
                levelProgress =
                    new NumberPushLevelProgress
                    {
                        LevelNumber = levelNumber
                    };

                progress.Levels.Add(
                    levelProgress);
            }

            levelProgress.Completed = true;
            levelProgress.LastPushes = pushes;

            if (!levelProgress.BestPushes.HasValue ||
                pushes < levelProgress.BestPushes.Value)
            {
                levelProgress.BestPushes = pushes;
            }

            int nextUnlockedLevel =
                levelNumber + 1;

            if (nextUnlockedLevel >
                progress.HighestUnlockedLevel)
            {
                progress.HighestUnlockedLevel =
                    nextUnlockedLevel;
            }

            progress.CurrentLevel =
                nextUnlockedLevel;

            Save(progress);
        }

        public NumberPushLevelProgress? GetLevelProgress(
            NumberPushProgress progress,
            int levelNumber)
        {
            return progress.Levels.FirstOrDefault(
                level =>
                    level.LevelNumber == levelNumber);
        }

        public void UpdateAvailableLevels(
            NumberPushProgress progress,
            List<int> availableLevelNumbers)
        {
            if (availableLevelNumbers.Count == 0)
            {
                return;
            }

            int highestAvailableLevel =
                availableLevelNumbers.Max();

            if (progress.HighestUnlockedLevel >
                highestAvailableLevel + 1)
            {
                progress.HighestUnlockedLevel =
                    highestAvailableLevel + 1;
            }

            if (!availableLevelNumbers.Contains(
                    progress.CurrentLevel))
            {
                int? nextAvailableLevel =
                    availableLevelNumbers
                        .Where(level =>
                            level >=
                            progress.CurrentLevel)
                        .Cast<int?>()
                        .FirstOrDefault();

                if (nextAvailableLevel.HasValue)
                {
                    progress.CurrentLevel =
                        nextAvailableLevel.Value;
                }
            }

            Save(progress);
        }
    }
}