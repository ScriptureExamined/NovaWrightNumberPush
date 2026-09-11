namespace NovaWrightNumberPush
{
    public class NumberPushLevelRepository
    {
        private readonly string levelsFolder;

        public NumberPushLevelRepository()
        {
            levelsFolder =
                Path.Combine(
                    AppContext.BaseDirectory,
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
            string filePath =
                GetLevelFilePath(
                    levelNumber);

            return File.Exists(
                filePath);
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