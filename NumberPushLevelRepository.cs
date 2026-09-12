namespace NovaWrightNumberPush
{
    public class NumberPushLevelRepository
    {
        private readonly string levelsFolder;

        public NumberPushLevelRepository()
        {
            string projectDirectory =
                Directory.GetParent(
                    AppContext.BaseDirectory)!
                .Parent!
                .Parent!
                .Parent!
                .FullName;

            levelsFolder =
                Path.Combine(
                    projectDirectory,
                    "Game",
                    "Levels");

            Directory.CreateDirectory(
                levelsFolder);
        }

        public List<int> GetAvailableLevelNumbers()
        {
            return Directory
                .GetFiles(
                    levelsFolder,
                    "Level*.json")
                .Select(
                    file =>
                    {
                        string fileName =
                            Path.GetFileNameWithoutExtension(
                                file);

                        string numberText =
                            fileName.Substring(
                                "Level".Length);

                        return int.TryParse(
                            numberText,
                            out int levelNumber)
                                ? levelNumber
                                : -1;
                    })
                .Where(
                    levelNumber =>
                        levelNumber >= 0)
                .OrderBy(
                    levelNumber =>
                        levelNumber)
                .ToList();
        }

        public bool LevelExists(
            int levelNumber)
        {
            return File.Exists(
                GetLevelFilePath(
                    levelNumber));
        }

        public NumberPushLevel LoadLevel(
            int levelNumber)
        {
            string filePath =
                GetLevelFilePath(
                    levelNumber);

            if (!File.Exists(
                    filePath))
            {
                throw new FileNotFoundException(
                    $"Level {levelNumber} could not be found.",
                    filePath);
            }

            return NumberPushLevelSerializer.LoadFromFile(
                filePath);
        }

        public void SaveLevel(
            NumberPushLevel level)
        {
            if (level == null)
            {
                throw new ArgumentNullException(
                    nameof(level));
            }

            if (level.LevelNumber <= 0)
            {
                throw new ArgumentException(
                    "The level must have a valid level number.",
                    nameof(level));
            }

            string filePath =
                GetLevelFilePath(
                    level.LevelNumber);

            NumberPushLevelSerializer.SaveToFile(
                level,
                filePath);
        }

        public void DeleteLevel(
            int levelNumber)
        {
            string filePath =
                GetLevelFilePath(
                    levelNumber);

            if (File.Exists(
                    filePath))
            {
                File.Delete(
                    filePath);
            }
        }

        public int? GetNextLevelNumber(
            int currentLevelNumber)
        {
            List<int> levelNumbers =
                GetAvailableLevelNumbers();

            return levelNumbers
                .Where(
                    levelNumber =>
                        levelNumber > currentLevelNumber)
                .OrderBy(
                    levelNumber =>
                        levelNumber)
                .Cast<int?>()
                .FirstOrDefault();
        }

        private string GetLevelFilePath(
            int levelNumber)
        {
            return Path.Combine(
                levelsFolder,
                $"Level{levelNumber:D3}.json");
        }
    }
}